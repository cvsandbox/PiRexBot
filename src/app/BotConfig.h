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

#ifndef PIREXBOT_CONFIG_H
#define PIREXBOT_CONFIG_H

/* IMPORTANT: All of the configuration below is done using physical pin numbering,
   not the GPIO logical numbering.
 */

// ===== LED's configuration =====

// Pin number for the LED, which is switched on while the PiRex Bot application is running
#define BOT_PIN_ON_LED (7)

// Pin number for the LED, which is switched on while there is an active connection to the bot
#define BOT_PIN_CONNECTION_ACTIVE_LED (11)

// ===== Motors' configuration (L293D chip in use) =====

// With the L293D, input1 and input2 control direction of motors' rotation. If input1 is set to HIGH,
// while input2 is set to LOW, a motor rotates "forward" (depends on how it is attached to the robot).
// And if input1 is LOW, while input2 is HIGH, a motor rotates opposite direction.

// Left motor
#define BOT_PIN_MOTOR_LEFT_ENABLE (12)
#define BOT_PIN_MOTOR_LEFT_INPUT1 (18)
#define BOT_PIN_MOTOR_LEFT_INPUT2 (16)

// Right motor
#define BOT_PIN_MOTOR_RIGHT_ENABLE (33)
#define BOT_PIN_MOTOR_RIGHT_INPUT1 (31)
#define BOT_PIN_MOTOR_RIGHT_INPUT2 (29)

// If software PWM is enabled for motors, then their speed can be controlled.
// If not enabled, then only state (i.e. rotate or not) and direction.
// #define BOT_MOTORS_ENABLE_SOFT_PWM

// ===== Distance measurements (HC-SR04 sensor in use) =====

// Tells if the bot is equipped with ultrasonic sensor for distance measurements
#define BOT_DISTANCE_ENABLE_MEASUREMENTS

#define BOT_PIN_ULTRASONIC_TRIGGER (22)
#define BOT_PIN_ULTRASONIC_ECHO    (37)

#endif // PIREXBOT_CONFIG_H
