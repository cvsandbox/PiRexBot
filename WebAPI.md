# Controlling robot from other applications (WEB API)

the PiRex bot provides API for interfacing with other applications, so that a native client could be developed and provide more advanced robot control or add extra features, like computer vision, for example. The first thing is to get video stream out of the robot, which is provided in the form of MJPEG stream. It does not provide the best compression - just a stream of individual JPEGs. However, it is very simple to implement and so supported by great variety of applications. The URL format to access MJPEG stream is:
```
http://ip:port/camera/mjpeg
```

In the case an individual image is required, the next URL provides the latest camera snapshot:
```
http://ip:port/camera/jpeg
```

### Getting version information
```
http://ip:port/version
```
To get information about version of the PiRex bot's software, the URL above is used, which provides information in the format below:
```JSON
{
  "status":"OK",
  "config":
  {
    "platform":"RaspberryPi",
    "product":"pirexbot",
    "version":"1.0.0"
  }
}
```

### Getting capabilities and title
```
http://ip:port/info
```
This API is used to find if PiRex bot is equipped with distance measurement sensor or allows speed control of motors. It also reports bot's title, which can be specified as one of the supported command line options.

```JSON
{
  "status":"OK",
  "config":
  {
    "device":"PiRex Bot",
    "providesDistance":"true",
    "providesSpeedControl":"false",
    "title":"My Home Robot"
  }
}
```

### Distance measuremen
```
http://ip:port/distance
```
The API is used querying distance measurements performed by PiRex robot. It provides as the most recent measurement in centimetres, as the median value taken from the last 5 measurements.

```JSON
{
  "status":"OK",
  "config":
  {
    "lastDistance":"128.95",
    "medianDistance":"127.25"
  }
}
```

### Controlling motors
```
http://ip:port/motors/config
```
Sending GET request to the above URL simply provides current state of the motors. This is not of much use though, since most of the time motors are stationary unless told to move.
```JSON
{
  "status":"OK",
  "config":
  {
    "leftPower":"0",
    "rightPower":"0"
  }
}
```

Sending a PUT request, however, is what's needed for telling robot to move. This is done with a simple JSON string, which tells power of both motors in the [-100, 100] range. In the case speed control is not enabled, there are only three possible values: 100 - rotate forward, 0 - don't move, -100 - rotate backward (although the robot will accept intermediate values as well, but threshold them). In the case if speed control is enabled, the speed value
can be anything from the mentioned range.

For example, the command below makes robot to rotate clockwise (rotate right).
```JSON
{
  "leftPower":"100",
  "rightPower":"-100"
}
```

### Camera configuration
```
http://ip:port/camera/config
```
Sending GET request to the above URL, provides the list of all camera's properties with their current values:

```JSON
{
  "status":"OK",
  "config":
  {
    "awb":"Auto",
    "brightness":"63",
    "contrast":"41",
    "effect":"None",
    "expmeteringmode":"Average",
    "expmode":"Night",
    "hflip":"1",
    "saturation":"16",
    "sharpness":"100",
    "vflip":"1",
    "videostabilisation":"0"
   }
}
```

For changing any of the properties, a POST request must be sent to the same URL, providing one or more configuration values to set. For example, below is the command for changing both brightness and contrast:

```JSON
{
  "brightness":"50",
  "contrast":"15"
}
```


### Camera information
```
http://ip:port/camera/info
```
This API provides some camera information. The most useful of which is its current resolution.
```JSON
{
  "status":"OK",
  "config":
  {
    "device":"RaspberryPi Camera",
    "title":"Front Camera",
    "width":"640",
    "height":"480"
  }
}
````

### Getting description of camera properties
```
http://ip:port/camera/properties
```
The API provides description of all supported camera's configuration properties - types of properties, titles, acceptable range of values, default value, etc. It was inherited from the cam2web project, where it does make sense, since that projects supports number of platforms and camera APIs. However, for PiRex it is of little use really - only one camera type is supported for now.

### Access rights
Accessing JPEG, MJPEG and robot's information URLs is available to those who have view access rights. Access to robot's configuration URLs (camera and motors) is available to those who have configuration access. The version URL is accessible to anyone. See Running PiRex for more information about access rights.
