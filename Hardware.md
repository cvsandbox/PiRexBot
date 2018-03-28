# Reference robot design

The are no strict requirements to the way how PiRex robot is built. Current design is based on Raspberry Pi board and camera module, L293D motor drive and HC-SR04 ultrasonic distance sensor. But how it is all connected and put together is completely customary, nothing is enforced by the provided software. It is assumed that anyone trying to get something similar working, has some basic knowledge of electronics, wiring, etc.

The HC-SR04 ultrasonic distance sensor is optional. If particular design does not assume performing distance measurement, the PiRex code can be configured to cope without it.

The L293D motor drive is a must for now. However, adding support for different motor drive is trivial and not a show-stopper.

The largest part of the PiRex code base is concentrated around interfacing with Raspberry Pi camera module and providing embedded web server, which allows controlling the robot from either a web browser or a dedicated native application. Since Raspberry Pi and its camera module is the core of the design, the provided software can be of use for any other robot sharing these components. Adding support for extra components/sensors is really easy to accomplish provided the current infrastructure and the L293D/HC-SR04 examples.
