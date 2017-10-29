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

#ifdef BOT_MOTORS_ENABLE_SOFT_PWM
    #include <softPwm.h>
#endif

using namespace std;

const static string  PROP_LEFT_POWER  = "leftPower";
const static string  PROP_RIGHT_POWER = "rightPower";

const static list<string> SupportedProperties = { PROP_LEFT_POWER, PROP_RIGHT_POWER };

MotorsController::MotorsController( ) :
    leftMotorPower( 0 ), rightMotorPower( 0 )
{
#ifdef BOT_MOTORS_ENABLE_SOFT_PWM
    softPwmCreate( BOT_PIN_MOTOR_LEFT_ENABLE, 0, 100 );
    softPwmCreate( BOT_PIN_MOTOR_RIGHT_ENABLE, 0, 100 );
#else
    pinMode( BOT_PIN_MOTOR_LEFT_ENABLE, OUTPUT );
    pinMode( BOT_PIN_MOTOR_RIGHT_ENABLE, OUTPUT );
#endif

    pinMode( BOT_PIN_MOTOR_LEFT_INPUT1, OUTPUT );
    pinMode( BOT_PIN_MOTOR_LEFT_INPUT2, OUTPUT );
 
    pinMode( BOT_PIN_MOTOR_RIGHT_INPUT1, OUTPUT );
    pinMode( BOT_PIN_MOTOR_RIGHT_INPUT2, OUTPUT );
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

// Helper function to set state/direction of motor controlled with L293D chip
static void SetMotorPower( int8_t power, uint8_t enablePin, uint8_t inputPin1, uint8_t inputPin2 )
{
    #ifndef BOT_MOTORS_ENABLE_SOFT_PWM
        if ( power > 0 )
        {
            power = ( power > 50 ) ? 100 : 0;
        }
        else
        {
            power = ( power < -50 ) ? -100 : 0;
        }
    #endif
    
    if ( power == 0 )
    {
        #ifdef BOT_MOTORS_ENABLE_SOFT_PWM
            softPwmWrite( enablePin, 0 );
        #else
            digitalWrite( enablePin, LOW );
        #endif
        digitalWrite( inputPin1, LOW );
        digitalWrite( inputPin2, LOW );
    }
    else
    {
        if ( power > 0 )
        {
            digitalWrite( inputPin1, HIGH );
            digitalWrite( inputPin2, LOW );
        }
        else
        {
            digitalWrite( inputPin1, LOW );
            digitalWrite( inputPin2, HIGH );
        }
        
        #ifdef BOT_MOTORS_ENABLE_SOFT_PWM
            softPwmWrite( enablePin, ( power > 0 ) ? power : -power  );
        #else
            digitalWrite( enablePin, HIGH );
        #endif
    }
}

// Set power of the left motor
void MotorsController::SetLeftPower( int8_t power )
{
    power = ( power > 100 ) ? 100 : ( ( power < -100 ) ? -100 : power );
        
    if ( leftMotorPower != power )
    {
        leftMotorPower = power;
        SetMotorPower( power, BOT_PIN_MOTOR_LEFT_ENABLE, BOT_PIN_MOTOR_LEFT_INPUT1, BOT_PIN_MOTOR_LEFT_INPUT2 );
    }
}

// Set power of the right motor
void MotorsController::SetRightPower( int8_t power )
{
    power = ( power > 100 ) ? 100 : ( ( power < -100 ) ? -100 : power );
    
    if ( rightMotorPower != power )
    {
        rightMotorPower = power;
        SetMotorPower( power, BOT_PIN_MOTOR_RIGHT_ENABLE, BOT_PIN_MOTOR_RIGHT_INPUT1, BOT_PIN_MOTOR_RIGHT_INPUT2 );
    }    
}

// Stop both motors
void MotorsController::MotorsController::Stop( )
{
    SetLeftPower( 0 );
    SetRightPower( 0 );
}

// Set property of the object
XError MotorsController::SetProperty( const string& propertyName, const string& value )
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
XError MotorsController::GetProperty( const string& propertyName, string& value ) const
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
