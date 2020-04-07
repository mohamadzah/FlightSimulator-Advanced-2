using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FlightSimulatorApp.Model
{
    interface IModel : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();

        //Dashboard properties
        double heading_deg { get; set; }
        double vertical_speed { get; set; }
        double ground_speed { get; set; }
        double indicated_groundSpeed { get; set; }
        double airSpeed { get; set; }
        double gps_altitude { get; set; }
        double roll_deg { get; set; }
        double pitch_deg { get; set; }
        double altimeter { get; set; }
    }
}
