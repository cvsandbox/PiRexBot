/*
    PiRexBot .NET Client - remote controlling of a RaspberryPi based PiRex robot.

    Copyright (C) 2018, cvsandbox, cvsandbox@gmail.com

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace PiRexBot
{
    internal enum HttpRequestType
    {
        Unknown,
        Get,
        Post
    };

    internal class HttpCommand
    {
        public uint            Id;
        public HttpRequestType Type;
        public string          Url;
        public string          Body;

        public HttpCommand( uint id, HttpRequestType type, string url ) :
            this( id, type, url, null )
        {
        }

        public HttpCommand( uint id, HttpRequestType type, string url, string body )
        {
            Id   = id;
            Type = type;
            Url  = url;
            Body = body;
        }
    }

    public class HttpCommandEventArgs
    {
        public readonly uint    Id;
        public readonly bool    Success;
        public readonly string  Message;

        public HttpCommandEventArgs( uint id, bool success, string message )
        {
            Id      = id;
            Success = success;
            Message = message;
        }
    }

    public delegate void HttpCommandCompletionHandler( object sender, HttpCommandEventArgs eventArgs );

    class HttpCommandsThread
    {
        // base address to send HTTP requests to
        // (starts with "http://" and followed by ip:port or DNS name; no final '/')
        private string baseAddress;
        // login and password for HTTP authentication
        private string login    = null;
        private string password = null;

        // command counter used to assigne IDs to requests
        private uint   commandCounter = 0;
        // queue of commands to process
        private List<HttpCommand> commandQueue = new List<HttpCommand>( );

        private Thread           thread       = null;
        private ManualResetEvent stopEvent    = null;
        private ManualResetEvent commandEvent = null;

        // dummy object to lock for synchronization
        private object sync = new object( );

        // timeout value for web request
        private const int requestTimeout = 5000;
        // buffer size used to read HTTP responses
        private const int bufferSize     = 1024 * 10;
        // size of portion to read at once
        private const int readSize       = 1024;

        // event to notify about completed HTTP command (successfully or not)
        public event HttpCommandCompletionHandler HttpCommandCompletion;

        public string BaseAddress
        {
            get { return baseAddress; }
            set { baseAddress = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public HttpCommandsThread( string baseAddress )
        {
            this.baseAddress = baseAddress;
        }

        // Start the thread so it is ready to handle HTTP requests
        public void Start( )
        {
            if ( !IsRunning )
            {
                if ( string.IsNullOrEmpty( baseAddress ) )
                {
                    throw new ArgumentException( "Base address is not specified for HTTP commands." );
                }

                stopEvent    = new ManualResetEvent( false );
                commandEvent = new ManualResetEvent( false );

                thread = new Thread( new ThreadStart( WorkerThread ) );
                thread.Start( );

                lock ( sync )
                {
                    if ( commandQueue.Count != 0 )
                    {
                        commandEvent.Set( );
                    }
                }
            }
        }

        // Signal the thread it needs to stop
        public void SignalToStop( )
        {
            if ( thread != null )
            {
                commandEvent.Set( );
                stopEvent.Set( );
            }
        }

        // Wait for the thread to stop
        public void WaitForStop( )
        {
            if ( thread != null )
            {
                thread.Join( );

                Free( );
            }
        }

        // Check if the background thread is running
        public bool IsRunning
        {
            get
            {
                if ( thread != null )
                {
                    // check thread status
                    if ( thread.Join( 0 ) == false )
                    {
                        return true;
                    }

                    // the thread is not running, so free resources
                    Free( );
                }
                return false;
            }
        }

        // Free resources of the background thread
        private void Free( )
        {
            commandEvent.Close( );
            stopEvent.Close( );

            stopEvent    = null;
            commandEvent = null;
            thread       = null;
        }

        // Add GET request to the queue
        public uint EnqueueGetRequest( string url )
        {
            return EnqueueGetRequest( url, false );
        }
        public uint EnqueueGetRequest( string url, bool clearPreviousRequests )
        {
            return EnqueueRequest( new HttpCommand( 0, HttpRequestType.Get, url ), clearPreviousRequests );
        }

        // Add POST request to the queue
        public uint EnqueuePostRequest( string url, string body )
        {
            return EnqueuePostRequest( url, body, false );
        }
        public uint EnqueuePostRequest( string url, string body, bool clearPreviousRequests )
        {
            return EnqueueRequest( new HttpCommand( 0, HttpRequestType.Post, url, body ), clearPreviousRequests );
        }

        // Clear all queued requests
        public void ClearRequestsQueue( )
        {
            lock ( sync )
            {
                commandQueue.Clear( );
                commandEvent.Reset( );
            }
        }

        // Clear queued requests for the specified URL
        public void ClearRequests( string url )
        {
            lock ( sync )
            {
                commandEvent.Reset( );

                commandQueue.RemoveAll( command => command.Url == url );

                if ( commandQueue.Count != 0 )
                {
                    commandEvent.Set( );
                }
            }
        }

        // Add the HTTP request to the internal queue and signal background thread to handle it
        private uint EnqueueRequest( HttpCommand command, bool clearPreviousRequests )
        {
            lock ( sync )
            {
                command.Id = ++commandCounter;

                if ( commandCounter == uint.MaxValue )
                {
                    commandCounter = 0;
                }

                if ( clearPreviousRequests )
                {
                    ClearRequests( command.Url );
                }

                commandQueue.Add( command );

                commandEvent.Set( );
            }

            return command.Id;
        }

        // Thread to handle HTTP requests
        private void WorkerThread( )
        {
            while ( !stopEvent.WaitOne( 0, false ) )
            {
                HttpCommand commandToProcess = null;

                commandEvent.WaitOne( );

                lock ( sync )
                {
                    if ( commandQueue.Count != 0 )
                    {
                        commandToProcess = commandQueue[0];
                        commandQueue.RemoveAt( 0 );
                    }

                    if ( commandQueue.Count == 0 )
                    {
                        commandEvent.Reset( );
                    }
                }

                if ( ( commandToProcess != null ) && ( commandToProcess.Type != HttpRequestType.Unknown ) )
                {
                    HandleHttpCommand( commandToProcess );
                }
            }
        }

        // Handle the specified HTTP command
        private void HandleHttpCommand( HttpCommand commandToProcess )
        {
            HttpWebRequest request  = null;
            WebResponse    response = null;
            Stream         stream   = null;
            byte[]         buffer   = new byte[bufferSize];
            int            read, totalRead = 0;

            try
            {
                request = (HttpWebRequest) WebRequest.Create( baseAddress + commandToProcess.Url );
                request.Timeout = requestTimeout;

                if ( ( !string.IsNullOrEmpty( login ) ) && ( password != null ) )
                {
                    request.Credentials = new NetworkCredential( login, password );
                }

                // send POST data
                if ( commandToProcess.Type == HttpRequestType.Post )
                {
                    byte[] data = Encoding.ASCII.GetBytes( commandToProcess.Body );

                    request.Method        = "POST";
                    request.ContentType   = "text/plain";
                    request.ContentLength = data.Length;

                    stream = request.GetRequestStream( );
                    stream.Write( data, 0, data.Length );

                    stream.Close( );
                    stream.Dispose( );
                    stream = null;
                }

                // get response and its stream
                response = request.GetResponse( );
                stream   = response.GetResponseStream( );
                stream.ReadTimeout = requestTimeout;

                // read response
                while ( !stopEvent.WaitOne( 0, false ) )
                {
                    int toRead = Math.Min( readSize, bufferSize - totalRead );

                    if ( toRead == 0 )
                    {
                        throw new ApplicationException( "Read buffer is too small" );
                    }

                    // read next portion from stream
                    if ( ( read = stream.Read( buffer, totalRead, toRead ) ) == 0 )
                    {
                        break;
                    }

                    toRead += read;
                }

                string responseString = System.Text.Encoding.UTF8.GetString( buffer );

                HttpCommandCompletion( this, new HttpCommandEventArgs( commandToProcess.Id, true, responseString ) );
            }
            catch ( Exception exception )
            {
                HttpCommandCompletion( this, new HttpCommandEventArgs( commandToProcess.Id, false, exception.Message ) );
            }
            finally
            {
                if ( request != null )
                {
                    request.Abort( );
                    request = null;
                }
                if ( stream != null )
                {
                    stream.Close( );
                    stream.Dispose( );
                    stream = null;
                }
                if ( response != null )
                {
                    response.Close( );
                    response = null;
                }
            }
        }
    }
}
