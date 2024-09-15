# FlightSimulator
FlightSimulator is a project that I created while attending the Advanced Programming 2 class at Bar-Ilan University
## Description and Design:
In this project, we developed a WPF application that uses the MVVM architecture, and implements a TCP Client which sends and receives data from FlightGear
by using the MVVM architecture, we were able to develop the app into three different sections.
*Model*: In the Model we are able to interact with the FlightGear server via a TCP connection over which we transmit and receive data and notify the *ViewModel* when data is updated.
Once a Data is updated, the *ViewModel* is responsibile for processing such data and notifying the *View*, once the *View* is notified the updated data is further processed and displayed.

**_Design and visuals_**: In this project we have a home screen and a main screen, 
- *Home screen*: In the home screen the user is presented with two buttons, a connect button that connects to either the default IP and Port given by the program, or a custom input by the user. And an exit button which closes the program.
  ![github picture](https://github.com/user-attachments/assets/7433465b-6323-4cf1-8f72-d937d24d9614)
- *Main activity screen*: In the main screen on the left side we have a Bing Map which we use to show the location of the flight, at the top left side we have the latitude and longitude coordinates of the flight, to the right we have a list of more data that is relevant to the flight such as it's airspeed, ground speed, altitude etc. And the main part is the two sliders and the Joystick which are used to control and adjust the flight's course ׁׁׂׂ(The Joystick xaml was imported and was not developed by me, as per the instructions of the class teacher at the time)
  ![ttete](https://github.com/user-attachments/assets/cd584597-2609-4b29-bc40-c7425e87db33)

### How to compile and run:
To use the program, there are a couple of steps you have to follow:
- *1*: Download all the files of this project including the dummy_server.py, and build_and_run.bat
- *2*: Create a ZIP called FlightSimulator, and move to it all the files except the dummy server and build_and_run.bat
- *3*: Create a folder called Test, move to it the ZIP file, dummy_server.py and build_and_run.bat
- *4*: Click on the dummy_server.py to run it (You need to have python installed)
- *5*: Run the build_and_run.bat, you should now see all the files extracted, click on the out folder and run FlightSimulatorApp.exe if it does not run automatically.
- *6*: Connect to the server using the Default IP and Port (Matching those of the dummy flightgear server), and enjoy!

**_Note_**: We used a dummy flightgear server to speed up the process of development and for the convenience, It imitates a server which sends and accepts the relevant data

#### Support

FlightGear simulator: https://www.flightgear.org/

###### Authors

***The author(s) of this program is Mohamad Zahalka***
