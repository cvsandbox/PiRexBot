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

#include "MotorsController.hpp"
#include "BotConfig.h"

#include <list>
#include <wiringPi.h>

using namespace std;

const static string  PROP_LEFT_POWER  = "leftPower";
const static string  PROP_RIGHT_POWER = "rightPower";

list<string> SupportedProperties = { PROP_LEFT_POWER, PROP_RIGHT_POWER };

MotorsController::MotorsController( ) :
    leftMotorPower( 0 ), rightMotorPower( 0 )
{
    pinMode( BOT_PIN_MOTOR1_ENABLE, OUTPUT );
    pinMode( BOT_PIN_MOTOR1_INPUT1, OUTPUT );
    pinMode( BOT_PIN_MOTOR1_INPUT2, OUTPUT );
 
    pinMode( BOT_PIN_MOTOR2_ENABLE, OUTPUT );
    pinMode( BOT_PIN_MOTOR2_INPUT1, OUTPUT );
    pinMode( BOT_PIN_MOTOR2_INPUT2, OUTPUT );
}

MotorsController::~MotorsController( )
{
    Stop( );
}

// Run motors at the specified speed [-100, 100]
void MotorsController::Run( int8_t leftPower, int8_t rightPower )
{
    SetLeftPower( leftPower );
    SetRightPower( rightPower );
}

// Set power of the left motor
void MotorsController::SetLeftPower( int8_t power )
{
    leftMotorPower = ( power > 100 ) ? 100 : ( ( power < -100 ) ? -100 : power );
    
    if ( leftMotorPower == 0 )
    {
        digitalWrite( BOT_PIN_MOTOR1_ENABLE, LOW );
        digitalWrite( BOT_PIN_MOTOR1_INPUT1, LOW );
        digitalWrite( BOT_PIN_MOTOR1_INPUT2, LOW );
    }
    else
    {
        if ( leftMotorPower > 0 )
        {
            digitalWrite( BOT_PIN_MOTOR1_INPUT1, HIGH );
            digitalWrite( BOT_PIN_MOTOR1_INPUT2, LOW );
        }
        else
        {
            digitalWrite( BOT_PIN_MOTOR1_INPUT1, LOW );
            digitalWrite( BOT_PIN_MOTOR1_INPUT2, HIGH );
        }
        digitalWrite( BOT_PIN_MOTOR1_ENABLE, HIGH );
    }
}

// Set power of the right motor
void MotorsController::SetRightPower( int8_t power )
{
    rightMotorPower = ( power > 100 ) ? 100 : ( ( power < -100 ) ? -100 : power );
    
    if ( rightMotorPower == 0 )
    {
        digitalWrite( BOT_PIN_MOTOR2_ENABLE, LOW );
        digitalWrite( BOT_PIN_MOTOR2_INPUT1, LOW );
        digitalWrite( BOT_PIN_MOTOR2_INPUT2, LOW );
    }
    else
    {
        if ( rightMotorPower > 0 )
        {
            digitalWrite( BOT_PIN_MOTOR2_INPUT1, HIGH );
            digitalWrite( BOT_PIN_MOTOR2_INPUT2, LOW );
        }
        else
        {
            digitalWrite( BOT_PIN_MOTOR2_INPUT1, LOW );
            digitalWrite( BOT_PIN_MOTOR2_INPUT2, HIGH );
        }
        digitalWrite( BOT_PIN_MOTOR2_ENABLE, HIGH );
    }    
}

// Stop both motors
void MotorsController::MotorsController::Stop( )
{
    SetLeftPower( 0 );
    SetRightPower( 0 );
}

// Set property of the object
XError MotorsController::SetProperty( const std::string& propertyName, const std::string& value )
{
    XError ret = XError::Success;
    int    numericValue;

    // motors configuration setting are all  numeric, so scan it
    int scannedCount = sscanf( value.c_str( ), "%d", &numericValue );
    
    if ( scannedCount != 1 )
    {
        ret = XError::InvalidPropertyValue;
    }
    else if ( propertyName == PROP_LEFT_POWER )
    {
        SetLeftPower( static_cast<int8_t>( numericValue ) );
    }
    else if ( propertyName == PROP_RIGHT_POWER )
    {
        SetRightPower( static_cast<int8_t>( numericValue ) );
    }
    else
    {
        ret = XError::UnknownProperty;
    }
    
    return ret;
}

// Get property of the object
XError MotorsController::GetProperty( const std::string& propertyName, std::string& value ) const
{
    XError ret = XError::Success;
    int    numericValue;

    if ( propertyName == PROP_LEFT_POWER )
    {
        numericValue = leftMotorPower;
    }
    else if ( propertyName == PROP_RIGHT_POWER )
    {
        numericValue = rightMotorPower;
    }
    else
    {
        ret = XError::UnknownProperty;
    }
    
    if ( ret == XError::Success )
    {
        char buffer[32];

        sprintf( buffer, "%d", numericValue );
        value = buffer;
    }
    
    return ret;
}

// Get all supported properties of the object
map<string, string> MotorsController::GetAllProperties( ) const
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
