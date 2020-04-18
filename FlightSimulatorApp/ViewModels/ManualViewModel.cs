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
    class ManualViewModel 
    {
        private IModel model;

        public ManualViewModel(IModel _model)
        {
            this.model = _model;
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

        public double VM_Throttle { set { model.Throttle = value; } get { return model.Throttle; } }
        public double VM_Rudder { set { model.Rudder = value; } get { return model.Rudder; } }
        public double VM_Elevator { set { model.Elevator = value; } get { return model.Elevator; } }
        public double VM_Aileron { set { model.Aileron = value; } get { return model.Aileron; } }

        //joystick x,y positions.

    }
}
