using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.Model
{
    //Interface
    public interface IModel : INotifyPropertyChanged
    {
        //connect to server
        void connect(string ip, int port);
        //disconnect from server
        void disconnect();
        //add a message to server to queue.
        void EnqueueMsg(double val, string message);
        //start threads to set/get values
        void start();      
        
        //The Model properties

        //Dashboard part.
        double Heading_deg { get; set; }
        double Vertical_speed { get; set; }
        double Ground_speed { get; set; }
        double AirSpeed { get; set; }
        double Gps_altitude { get; set; }
        double Roll_deg { get; set; }
        double Pitch_deg { get; set; }
        double Altimeter { get; set; }

        //Map part
        double Longitude_deg { get; set; }
        double Latitude_deg { get; set; }
        Location Location { get; set; }

        //Flight control part.
        double Throttle { get; set; }
        double Rudder { get; set; }
        double Elevator { get; set; }
        double Aileron { get; set; }

        string Error { get; set; }

        //Settings
        int Port { get; set; }
        string Ip { get; set; }
    }
}
