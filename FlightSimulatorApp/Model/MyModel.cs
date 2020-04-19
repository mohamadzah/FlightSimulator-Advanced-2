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

        //Constructor
        public MyModel(ITelnetClient _telnetClient) {
            this.telnetClient = _telnetClient;
            setQueue = new Queue<string>();
        }

        //Model properties 
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
        private Location location;

        private double throttle;
        private double rudder;
        private double elevator;
        private double aileron;

        private string error;
        private bool status;

        private int port;
        private string ip;
        //Implementation of set/get for the model properties.
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
                    this.setQueue.Enqueue("set /controls/flight/aileron " + value + "\n");
                }
            }
        }
      
        public Location Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public int Port
        {
            get { return this.port; }
            set
            {             
                this.port = value;
                NotifyPropertyChanged("Port");
            }
        }

        public string Ip
        {
            get { return this.ip; }
            set
            {
                this.ip = value;
                NotifyPropertyChanged("Ip");
            }
        }

        public string Error
        {
            get { return this.error; }
            set
            {
                this.error = value;
                NotifyPropertyChanged("Error");
            }
        }

        public bool Status
        {
            get { return this.status; }
            set
            {
                this.status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public void connect(string ip, int port) {
            try
            {
                telnetClient.connect(ip, port);
                this.start();
                Console.WriteLine("Connected!");
                status = true;
            }

            catch (Exception)
            {
                Console.WriteLine("Can't connect to server!");
                status = false;
                this.Error = "initiateERR";
            }
        }

        public void disconnect()
        {

            while (setQueue.Count != 0) { }

            stop = true;

            if (setQueue.Count == 0)
            {
                Thread.Sleep(50);
                telnetClient.disconnect();
            } 
            else
            {
                Console.WriteLine("F");
            }
        }

        public void EnqueueMsg(double val, string message)
        {
            setQueue.Enqueue(message + val + "\n");
        }

        public void start() {
            new Thread(delegate ()
            {
                Heading_deg = 0;
                AirSpeed = 0;
                Ground_speed = 0;
                Altimeter = 0;
                Gps_altitude = 0;
                Roll_deg = 0;
                Pitch_deg = 0;
                Vertical_speed = 0;
                Latitude_deg = 0;
                Longitude_deg = 0;

                string readMessage = String.Empty;
                int flag = 0;
                while (!stop)
                {
                    flag = 0; 
                    //as long as the server is not disconnected, keep reading values.
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");                   
                    readMessage = telnetClient.read();

                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Heading_deg = Double.Parse(readMessage);
                    }
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    readMessage = telnetClient.read();
                 
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Vertical_speed = Double.Parse(readMessage);
                    }
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    readMessage = telnetClient.read();
                 
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Ground_speed = Double.Parse(readMessage);
                    }
               
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    readMessage = telnetClient.read();
                
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        AirSpeed = Double.Parse(readMessage);
                    }                 
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    readMessage = telnetClient.read();
               
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Gps_altitude = Double.Parse(readMessage);
                    }
   
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    readMessage = telnetClient.read();
                 
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Roll_deg = Double.Parse(readMessage);
                    }

                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    readMessage = telnetClient.read();
                  
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Pitch_deg = Double.Parse(readMessage);
                    }

                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    readMessage = telnetClient.read();
                  
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Altimeter = Double.Parse(readMessage);
                    }
  
                    mutex.ReleaseMutex();
                    mutex.WaitOne();
                    telnetClient.write("get /position/longitude-deg\n");
                    readMessage = telnetClient.read();
       
                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        flag = 1;
                        Longitude_deg = Double.Parse(readMessage);
                    }

                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /position/latitude-deg\n");
                    readMessage = telnetClient.read();

                    if ((!readMessage.Contains("ERR")) && (!readMessage.Contains("CRITICAL")))
                    {
                        Latitude_deg = Double.Parse(readMessage);
                        if (flag == 1)
                        {
                            //if the recieved longitude and latitude are within the valid range for these values.
                            if ((Latitude_deg >= -90) && (Latitude_deg <= 90))
                            {
                                Location = new Location(Latitude_deg, Longitude_deg);
                            }
                        }
                    }

                    mutex.ReleaseMutex();
                    //4 times        

                    Thread.Sleep(250);              
                }

            }).Start();

            //keep sending the values using the joystick.
            new Thread(delegate ()
            {
                while (!stop)
                {
                    if (setQueue.Count != 0)
                    {
                        mutex.WaitOne();
                        string messageSend = setQueue.Dequeue();
                        telnetClient.write(messageSend);
                        string r = telnetClient.read();
                        mutex.ReleaseMutex();
                        Console.WriteLine(r);
                    }
                    Thread.Sleep(1);
                }
            }).Start();
        }
    }
}
