using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
using System.ComponentModel;
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
        public void NotifyPropertyChanged(string propName) { }
    }

    //properties, set get etc.. later once i figure out what we need 100%.
}
