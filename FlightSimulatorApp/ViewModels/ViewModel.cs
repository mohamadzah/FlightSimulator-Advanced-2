using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.ViewModels

{
    //the view model recieves the commands, and commands the model.
    //has to implement Inotifypropertychanged.
    class ViewModel : INotifyPropertyChanged
    {
        private IModel model;
        public ManualViewModel mvm;
        public PlaneViewModel pvm;
        public DashBoardViewModel dvm;
        private int port;
        private string ip;

        public ViewModel(IModel _model) {
            this.model = _model;
            this.mvm = new ManualViewModel(this.model);
            this.dvm = new DashBoardViewModel(this.model);
            this.pvm = new PlaneViewModel(this.model);

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }   

        public string VM_ip
        {
            set
            {
                this.ip = value;
            }
        }

        public int VM_port
        {
            set
            {
                this.port = value;
            }
        }

        public void Connect()
        {
            this.model.connect(ip, port);
        }

        public void Disconnect()
        {
            this.model.disconnect();
        }

    }

    //properties, set get etc..
}
