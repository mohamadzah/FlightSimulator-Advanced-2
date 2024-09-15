# FlightSimulator
FlightSimulator is a project that i created while attending the Advanced Programming 2 class at Bar-Ilan University
## Description:
In this project, we developed a WPF application that uses the MVVM architecture, and implements a TCP Client which sends and receives data from FlightGear
By using the MVVM architecture, we were able to develop the app into three different sections.
*Model*: In the Model we are able to interact with the FlightGear server via a TCP connection over which we transmit and receive data and notify the *ViewModel* when data is updated.
Once a Data is updated, the *ViewModel* is responsibile for processing such data and notifies the *View*, once the *View* is notified the updated data is processed and displayed.
