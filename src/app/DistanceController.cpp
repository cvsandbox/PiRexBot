/*
    PiRexBot - remote controlled bot based on RaspberryPi

    Copyright (C) 2017, cvsandbox, cvsandbox@gmail.com

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

#include "DistanceController.hpp"
#include "BotConfig.h"

#include <stdio.h>
#include <stdint.h>
#include <list>
#include <mutex>
#include <thread>
#include <wiringPi.h>

#include "XManualResetEvent.hpp"

using namespace std;

// Time (microseconds) to wait for distance measurement
#define MEASUREMENT_TIMEOUT_US  (15000)

const static string  PROP_LAST_DISTANCE    = "lastDistance";
const static string  PROP_MEDIAN_DISTANCE  = "meadianDistance";

const static list<string> SupportedProperties = { PROP_LAST_DISTANCE, PROP_MEDIAN_DISTANCE };

namespace Private
{
    // Private implementation details for the DistanceController
    class DistanceControllerData
    {
    public:
        float                   LastDistance;
        float                   MedianDistance;

        recursive_mutex         Sync;
        thread                  ControlThread;
        XManualResetEvent       NeedToStop;
        bool                    Running;

    public:
        DistanceControllerData( ) :
            LastDistance( 0 ), MedianDistance( 0 ),
            Sync( ), ControlThread( ), NeedToStop( ), Running( false )
        {
        }

        bool StartMeasurements( );
        void StopMeasurements( );
        bool IsRunning( );

        static void ControlThreadHanlder( DistanceControllerData* me );
    };
}

DistanceController::DistanceController( ) :
    mData( new Private::DistanceControllerData )
{
}

DistanceController::~DistanceController( )
{
    delete mData;
}

// Start background thread running distance measurements
bool DistanceController::StartMeasurements( )
{
    return mData->StartMeasurements( );
}

// Stop background thread and so the distance measurements
void DistanceController::StopMeasurements( )
{
    mData->StopMeasurements( );
}

// Check if measurements thread is running
bool DistanceController::IsRunning( )
{
    return mData->IsRunning( );
}

// Value of the last measurement
float DistanceController::Distance( )
{
}

// Median value of the distance
float DistanceController::MedianDistance( )
{
}

// Get property of the object
XError DistanceController::GetProperty( const string& propertyName, string& value ) const
{
    XError ret = XError::Success;
    float  numericValue;

    if ( propertyName == PROP_LAST_DISTANCE )
    {
        numericValue = mData->LastDistance;
    }
    else if ( propertyName == PROP_MEDIAN_DISTANCE )
    {
        numericValue = mData->MedianDistance;
    }
    else
    {
        ret = XError::UnknownProperty;
    }

    if ( ret == XError::Success )
    {
        char buffer[32];

        sprintf( buffer, "%0.2f", numericValue );
        value = buffer;
    }

    return ret;
}

// Get all supported properties of the object
map<string, string> DistanceController::GetAllProperties( ) const
{
    map<string, string> properties;
    string              value;

    for ( auto property : SupportedProperties )
    {
        if ( GetProperty( property, value ) )
        {
            properties.insert( pair<string, string>( property, value ) );
        }
    }

    return properties;
}

namespace Private
{

// Start measurements thread
bool DistanceControllerData::StartMeasurements( )
{
    lock_guard<recursive_mutex> lock( Sync );

    if ( !IsRunning( ) )
    {
        NeedToStop.Reset( );
        Running = true;

        LastDistance   = 0.0f;
        MedianDistance = 0.0f;

        ControlThread = thread( ControlThreadHanlder, this );
    }

    return true;
}

// Stop measurements thread
void DistanceControllerData::StopMeasurements( )
{
    lock_guard<recursive_mutex> lock( Sync );

    NeedToStop.Signal( );

    if ( ( IsRunning( ) ) || ( ControlThread.joinable( ) ) )
    {
        ControlThread.join( );
    }
}

// Check if the measurements thread is running
bool DistanceControllerData::IsRunning( )
{
    lock_guard<recursive_mutex> lock( Sync );

    if ( ( !Running ) && ( ControlThread.joinable( ) ) )
    {
        ControlThread.join( );
    }

    return Running;
}

// Measurements thread handler
void DistanceControllerData::ControlThreadHanlder( DistanceControllerData* me )
{
    uint32_t reftime, start, stop;
    bool     failed;

    pinMode( BOT_PIN_ULTRASONIC_TRIGGER, OUTPUT );
    pinMode( BOT_PIN_ULTRASONIC_ECHO, INPUT );

    while ( !me->NeedToStop.Wait( 10 ) )
    {
        failed = false;

        // trigger measurement round
        digitalWrite( BOT_PIN_ULTRASONIC_TRIGGER, HIGH );
        delayMicroseconds( 10 );
        digitalWrite( BOT_PIN_ULTRASONIC_TRIGGER, LOW );

        reftime = micros( );
        start   = reftime;

        // wait for the echo wave start
        while ( digitalRead( BOT_PIN_ULTRASONIC_ECHO ) == LOW )
        {
            start = micros( );
            if ( start - reftime > MEASUREMENT_TIMEOUT_US )
            {
                failed = true;
                break;
            }
        }

        if ( !failed )
        {
            reftime = micros( );
            stop    = reftime;

            // wait for the echo wave stop
            while ( digitalRead( BOT_PIN_ULTRASONIC_ECHO ) == HIGH )
            {
                stop = micros( );
                if ( stop - reftime > MEASUREMENT_TIMEOUT_US )
                {
                    failed = true;
                    break;
                }
            }
        }

        if ( !failed )
        {
            me->LastDistance = (float) ( stop - start ) / 58.2f;

            printf( "%0.2f\n", me->LastDistance );
        }
    }

    pinMode( BOT_PIN_ULTRASONIC_ECHO, OUTPUT );

    digitalWrite( BOT_PIN_ULTRASONIC_TRIGGER, LOW );
    digitalWrite( BOT_PIN_ULTRASONIC_ECHO, LOW );
}

} // namespace Private
