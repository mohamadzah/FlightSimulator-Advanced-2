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
    public interface IModel : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();

        //The Model properties
        Location Location { get; set; }

        double Heading_deg { get; set; }
        double Vertical_speed { get; set; }
        double Ground_speed { get; set; }
        double AirSpeed { get; set; }
        double Gps_altitude { get; set; }
        double Roll_deg { get; set; }
        double Pitch_deg { get; set; }
        double Altimeter { get; set; }
        double Longitude_deg { get; set; }
        double Latitude_deg { get; set; }

        double Throttle { get; set; }
        double Rudder { get; set; }
        double Elevator { get; set; }
        double Aileron { get; set; }

    }
}
