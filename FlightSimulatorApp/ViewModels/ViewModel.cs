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
    /// <summary>
    /// ViewModel (settings view model)
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        private IModel model;
        private int port;
        private string ip;

        //Constructor.
        public ViewModel(IModel _model) {
            this.model = _model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }   

        //set the ip
        public string VM_Ip
        {
            set
            {
                this.ip = value;
            }
        }

        //set the port
        public int VM_Port
        {
            set
            {
                this.port = value;
            }
        }

        public string VM_Error
        {
            get { return model.Error; }
            set
            {
                model.Error = value;
            }
        }

        public bool VM_Status
        {
            get { return model.Status; }
        }

        //Connect to server.
        public void Connect()
        {
            this.model.connect(ip, port);
        }

        //Disconnect from server
        public void Disconnect()
        {
            this.model.disconnect();
        }

        //Start the threads
        public void Start()
        {
            this.model.start();
        }

    }

    //properties, set get etc..
}
