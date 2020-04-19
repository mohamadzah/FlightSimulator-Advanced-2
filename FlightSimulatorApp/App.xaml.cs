using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // APP STARTUP
        private IModel model;
        public ViewModel vm;
        public ManualViewModel manualViewModel;
        public PlaneViewModel planeViewModel;
        public DashBoardViewModel boardViewModel;
        public FlightMainPage main;
        public Home homePage;

        void App_Startup(object sender, StartupEventArgs e)
        {
            model = new MyModel(new MyTelnetClient());
            vm = new ViewModel(this.model);
            this.manualViewModel = new ManualViewModel(this.model);
            this.boardViewModel = new DashBoardViewModel(this.model);
            this.planeViewModel = new PlaneViewModel(this.model);
            this.main = new FlightMainPage();
            this.homePage = new Home();
            this.homePage.Show();
        }
    }
}
