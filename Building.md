# Building the PiRex source code

Before PiRex can be built in release configuration, it is required to build web2h tool provided with it, which translates some of the common web files (HTML, CSS, JS, JPEG and PNG) into header files. Those are then compiled and linked into the PiRex executable, so it could provide default web interface without relying on external files.

If building in debug configuration however, the web2h is not required – all web content is served from files located in ./web folder (which is populated automatically during build process).

Another thing to keep in mind before starting a build is the **BotConfig.h** header file. It contains number of #define's allowing to specify which Raspberry Pi’s pins are used for which purpose. Changes to this file are required only in the case if target robot build is different from the reference design.

Makefiles for GNU make are provided for both web2h and PiRex. Running bellow commands from the project’s root folder, will produce the required executables in **build/release/bin**.
```Bash
pushd .
cd src/tools/web2h/
make
popd

pushd .
cd src/app/
make
popd
```
Note: libjpeg development library must be installed for PiRex build to succeed (which may not be installed by default) :
```
sudo apt-get install libjpeg-dev
```
