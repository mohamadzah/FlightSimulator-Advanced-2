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
        public volatile Boolean stop = false;
        private Mutex mutex = new Mutex();
        private Queue<string> setQueue;
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
            setQueue = new Queue<string>();
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
                    NotifyPropertyChanged("Heading_deg");
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
                    NotifyPropertyChanged("Vertical_speed");
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
                    NotifyPropertyChanged("Ground_speed");
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
                    NotifyPropertyChanged("AirSpeed");
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
                    NotifyPropertyChanged("Gps_altitude");
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
                    NotifyPropertyChanged("Roll_deg");
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
                    NotifyPropertyChanged("Pitch_deg");
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
                    NotifyPropertyChanged("Altimeter");
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
                    NotifyPropertyChanged("Longitude_deg");
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
                    NotifyPropertyChanged("Latitude_deg");
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
                    //mutex.WaitOne();
                    //Console.WriteLine(value);
                    //telnetClient.write("set /controls/engines/current-engine/throttle " + value.ToString() + "\n");
                    //telnetClient.read();
                    //mutex.ReleaseMutex();
                    this.setQueue.Enqueue("set /controls/engines/current-engine/throttle " + value + "\n");
                }
            }
        }

        public double Rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
                    //mutex.WaitOne();
                    //Console.WriteLine(value);
                    //telnetClient.write("set /controls/flight/rudder " + value.ToString() + "\n");
                    //telnetClient.read();
                    //mutex.ReleaseMutex();
                    this.setQueue.Enqueue("set /controls/flight/rudder " + value + "\n");
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
                    //mutex.WaitOne();
                    //Console.WriteLine(value);
                    //telnetClient.write("set /controls/flight/elevator " + value.ToString() + "\n");
                    //telnetClient.read();
                    //mutex.ReleaseMutex();
                    this.setQueue.Enqueue("set /controls/flight/elevator " + value + "\n");
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
                    //mutex.WaitOne();
                    //Console.WriteLine(value);
                    //telnetClient.write("set /controls/flight/aileron " + value.ToString() + "\n");
                    //telnetClient.read();
                    //mutex.ReleaseMutex();
                    this.setQueue.Enqueue("set /controls/flight/aileron " + value + "\n");
                }
            }
        }

        private Location location;
        public Location Location
        {
            //maybe more work here
            get { return this.location; }
            set
            {
                this.location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public void disconnect() {
            stop = true;
            telnetClient.disconnect();
        }

        public void connect(string ip, int port) {
            try
            {
                telnetClient.connect(ip, port);
                this.start();
                Console.WriteLine("Connected!");
            }
            catch (Exception)
            {
                Console.WriteLine("Can't connect to server!");
            }
        }

        public void EnqueueMsg(double val, string message)
        {
            setQueue.Enqueue(message + val + "\n");
        }

        public void start() {
            new Thread(delegate ()
            {
                string lol = String.Empty;
                while (!stop)
                {
                    mutex.WaitOne();
                    //as long as the server is not disconnected, keep reading values.

                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                        Heading_deg = Double.Parse(lol);
                    }
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                        Vertical_speed = Double.Parse(lol);
                    }
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {

                        Ground_speed = Double.Parse(lol);

                    }
                    //   Ground_speed = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {

                        AirSpeed = Double.Parse(lol);
                    }

                    //   AirSpeed = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                                             
                            Gps_altitude = Double.Parse(lol);
                        
                    }
                    //    Gps_altitude = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    lol = telnetClient.read();
                  //  Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                        
                            Roll_deg = Double.Parse(lol);
                        
                    }
                    //   Roll_deg = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                       
                            Pitch_deg = Double.Parse(lol);
                        
                    }
                    //    Pitch_deg = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    lol = telnetClient.read();
                  //  Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                        
                            Altimeter = Double.Parse(lol);
                        
                    }
                    //  Altimeter = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /position/longitude-deg\n");
                    lol = telnetClient.read();
                  //  Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                        
                           Longitude_deg = Double.Parse(lol);
                        
                    }
                    //   Longitude_deg = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /position/latitude-deg\n");
                    lol = telnetClient.read();
                   // Console.WriteLine(lol);
                    if (!lol.Contains("ERR"))
                    {
                        Latitude_deg = Double.Parse(lol);
                    }
                    // Latitude_deg = Double.Parse(telnetClient.read());
                    mutex.ReleaseMutex();
                    //4 times        

                    Thread.Sleep(250);              
                }

            }).Start();

            new Thread(delegate ()
            {
                while (!stop)
                {
                    if (setQueue.Count != 0)
                    {
                        mutex.WaitOne();
                        telnetClient.write(setQueue.Dequeue());
                        Console.WriteLine("Message recieve :");
                        string r = telnetClient.read();
                        mutex.ReleaseMutex();
                        Console.WriteLine(r);
                    }                   
                }
            }).Start();
        }
    }
}
