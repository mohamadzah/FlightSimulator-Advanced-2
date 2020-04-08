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

        public ViewModel(IModel _model) {
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

        //View Model Properties
        public double VM_Heading_deg { get { return model.Heading_deg; }  }
        public double VM_Vertical_speed { get { return model.Vertical_speed; }  }
        public double VM_Ground_speed { get { return model.Ground_speed; } }
        public double VM_AirSpeed { get { return model.AirSpeed;  } }
        public double VM_Gps_altitude { get { return model.Gps_altitude; } }
        public double VM_Roll_deg { get { return model.Roll_deg;  } }
        public double VM_Pitch_deg { get { return model.Pitch_deg;  } }
        public double VM_Altimeter { get { return model.Altimeter; } }
        public double VM_Longitude_deg { get { return model.Longitude_deg; } }
        public double VM_Latitude_deg { get { return model.Latitude_deg; } }

        public double VM_Throttle { set { model.Throttle = value; } }
        public double VM_Rudder { set { model.Rudder = value ; } }
        public double VM_Elevator { set { model.Elevator = value; } }
        public double VM_Aileron { set { model.Aileron = value; } }

        public Location VM_Location { get { return model.Location; } }
    }

    //properties, set get etc..
}
