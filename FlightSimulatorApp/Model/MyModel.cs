using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.Model
{
    class MyModel : IModel
    {
        ITelnetClient telnetClient;
        volatile Boolean stop;

        //Implement INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) 
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public MyModel(ITelnetClient _telnetClient) {
            this.telnetClient = _telnetClient;
        }

        private double heading_deg;
        private double vertical_speed;
        private double ground_speed;
        private double airSpeed;
        private double gps_altitude;
        private double roll_deg;
        private double pitch_deg;
        private double altimeter;
        private double longitude_deg;
        private double latitude_deg;
        private double throttle;
        private double rudder;
        private double elevator;
        private double aileron;

        public double Heading_deg
        {
            get { return this.heading_deg; }
            set
            {
                if (this.heading_deg != value)
                {
                    this.heading_deg = value;
                    this.NotifyPropertyChanged("Heading_deg");
                }
            }
        }

        public double Vertical_speed
        {
            get { return this.vertical_speed; }
            set
            {
                if (this.vertical_speed != value)
                {
                    this.vertical_speed = value;
                    this.NotifyPropertyChanged("Vertical_speed");
                }
            }
        }

        public double Ground_speed
        {
            get { return this.ground_speed; }
            set
            {
                if (this.ground_speed != value)
                {
                    this.ground_speed = value;
                    this.NotifyPropertyChanged("Ground_speed");
                }
            }
        }

        public double AirSpeed
        {
            get { return this.airSpeed; }
            set
            {
                if (this.airSpeed != value)
                {
                    this.airSpeed = value;
                    this.NotifyPropertyChanged("AirSpeed");
                }
            }
        }

        public double Gps_altitude
        {
            get { return this.gps_altitude; }
            set
            {
                if (this.gps_altitude != value)
                {
                    this.gps_altitude = value;
                    this.NotifyPropertyChanged("Gps_altitude");
                }
            }
        }

        public double Roll_deg
        {
            get { return this.roll_deg; }
            set
            {
                if (this.roll_deg != value)
                {
                    this.roll_deg = value;
                    this.NotifyPropertyChanged("Roll_deg");
                }
            }
        }

        public double Pitch_deg
        {
            get { return this.pitch_deg; }
            set
            {
                if (this.pitch_deg != value)
                {
                    this.pitch_deg = value;
                    this.NotifyPropertyChanged("Pitch_deg");
                }
            }
        }

        public double Altimeter
        {
            get { return this.altimeter; }
            set
            {
                if (this.altimeter != value)
                {
                    this.altimeter = value;
                    this.NotifyPropertyChanged("Altimeter");
                }
            }
        }

        public double Longitude_deg
        {
            get { return this.longitude_deg; }
            set
            {
                if (this.longitude_deg != value)
                {
                    this.longitude_deg = value;
                    this.NotifyPropertyChanged("Longitude_deg");
                }
            }
        }

        public double Latitude_deg
        {
            get { return this.latitude_deg; }
            set
            {
                if (this.latitude_deg != value)
                {
                    this.latitude_deg = value;
                    this.NotifyPropertyChanged("Latitude_deg");
                }
            }
        }

        public double Throttle
        {
            get { return this.throttle; }
            set
            {
                if (this.throttle != value)
                {
                    this.throttle = value;
                    this.NotifyPropertyChanged("Throttle");
                }
            }
        }

        public double Rudder
        {
            get { return this.Rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
                    this.NotifyPropertyChanged("Rudder");
                }
            }
        }

        public double Elevator
        {
            get { return this.elevator; }
            set
            {
                if (this.elevator != value)
                {
                    this.elevator = value;
                    this.NotifyPropertyChanged("Elevator");
                }
            }
        }

        public double Aileron
        {
            get { return this.aileron; }
            set
            {
                if (this.aileron != value)
                {
                    this.aileron = value;
                    this.NotifyPropertyChanged("Aileron");
                }
            }
        }

        Location location;
        public Location Location
        {
            get { return this.Location; }
            set
            {
                this.location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public void disconnect() {
            telnetClient.disconnect();
            stop = true;
        }

        public void connect(string ip, int port) {
            try
            {
                telnetClient.connect(ip, port);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't connect to server!");
            }
        }

        public void start() {
            new Thread(delegate ()
            {
                while (!stop)
                {

                }

            }).Start();
        }
    }
}
