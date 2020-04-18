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
    class PlaneViewModel : INotifyPropertyChanged
    {
        private IModel model;

        public PlaneViewModel(IModel _model)
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

        public double VM_Longitude_deg { get { return model.Longitude_deg; } }

        public double VM_Latitude_deg { get { return model.Latitude_deg; } }

        public Location VM_Location { get { return model.Location; } }

    }
}
