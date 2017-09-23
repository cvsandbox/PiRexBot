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

#ifndef PIREXBOT_CONFIG_H
#define PIREXBOT_CONFIG_H

// ===== LEDs 'configuration =====

// Pin number for the LED, which is switched on while the PiRexBot application runs
#define BOT_PIN_ON_LED (7)

// Pin number for the LED, which is switched on while there is an active connection to the bot
#define BOT_PIN_CONNECTION_ACTIVE_LED (11)

// ===== Motors' configuration =====

#define BOT_PIN_MOTOR1_ENABLE (12)
#define BOT_PIN_MOTOR1_INPUT1 (18)
#define BOT_PIN_MOTOR1_INPUT2 (16)

#define BOT_PIN_MOTOR2_ENABLE (33)
#define BOT_PIN_MOTOR2_INPUT1 (31)
#define BOT_PIN_MOTOR2_INPUT2 (29)

#endif // PIREXBOT_CONFIG_H
