/*
    PiRexBot - remote controlled bot based on RaspberryPi

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

#ifndef MOTORS_CONTROLLER_HPP
#define MOTORS_CONTROLLER_HPP

#include <stdint.h>
#include <IObjectConfigurator.hpp>

// Class to manage motors speed/direction
class MotorsController : public IObjectConfigurator
{
public:
    MotorsController( );
    ~MotorsController( );
    
    // Run motors at the specified speed [-100, 100]
    void Run( int8_t leftPower, int8_t rightPower );
    void SetLeftPower( int8_t power );
    void SetRightPower( int8_t power );
    // Stop both motors
    void Stop( );

    // IObjectConfigurator implementation
    XError SetProperty( const std::string& propertyName, const std::string& value );
    XError GetProperty( const std::string& propertyName, std::string& value ) const;
    std::map<std::string, std::string> GetAllProperties( ) const;
    
private:
    int8_t leftMotorPower;
    int8_t rightMotorPower;
};

#endif // MOTORS_CONTROLLER_HPP
