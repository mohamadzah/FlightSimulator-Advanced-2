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
    /// <summary>
    /// DashBoardViewModel class.
    /// </summary>
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        private IModel model;

        //Constructor.
        public DashBoardViewModel(IModel _model)
        {
            this.model = _model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };    
        }
        //Implement NotifyPropertyChanged.
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Get/Set properties for dashboard. 
        public double VM_Heading_deg { get { return model.Heading_deg; }  set { } }
        public double VM_Vertical_speed { get { return model.Vertical_speed; } set { } }
        public double VM_Ground_speed { get { return model.Ground_speed; } set { } }
        public double VM_AirSpeed { get { return model.AirSpeed; } set { } }
        public double VM_Gps_altitude { get { return model.Gps_altitude; } set { } }
        public double VM_Roll_deg { get { return model.Roll_deg; } set { } }
        public double VM_Pitch_deg { get { return model.Pitch_deg; } set { } }
        public double VM_Altimeter { get { return model.Altimeter; } set { } }

    }
}
