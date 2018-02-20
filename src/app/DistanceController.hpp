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

#ifndef DISTANCE_CONTROLLER_HPP
#define DISTANCE_CONTROLLER_HPP

#include <IObjectInformation.hpp>

namespace Private
{
    class DistanceControllerData;
}

// Class to take distance measurement using ultrasonic sensor
class DistanceController : public IObjectInformation
{
public:
    DistanceController( );
    ~DistanceController( );

    // Start/stop measurements
    bool StartMeasurements( );
    void StopMeasurements( );
    bool IsRunning( );

    // Value of the last measurement (must be started)
    float Distance( );
    // Median value of the distance (over last 5 measurements)
    float MedianDistance( );

    // IObjectConfigurator implementation
    XError GetProperty( const std::string& propertyName, std::string& value ) const;
    std::map<std::string, std::string> GetAllProperties( ) const;

private:
    Private::DistanceControllerData* mData;
};

#endif // DISTANCE_CONTROLLER_HPP
