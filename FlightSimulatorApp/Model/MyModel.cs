using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    class MyModel : IModel
    {
        ITelnetClient telnetClient;
        volatile Boolean stop;

        //Implement INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) { }

        public MyModel(ITelnetClient _telnetClient) {
            this.telnetClient = _telnetClient;
        }

        public void disconnect() {
            telnetClient.disconnect();
        }

        public void connect(string ip, int port) {
            telnetClient.connect(ip, port);
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
