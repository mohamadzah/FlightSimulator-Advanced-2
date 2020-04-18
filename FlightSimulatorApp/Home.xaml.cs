using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.View;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        // APP STARTUP
        private IModel model;
        ViewModel vm;
        ManualViewModel mvm;
        PlaneViewModel pvm;
        DashBoardViewModel dvm;

        public Home()
        {
            InitializeComponent();
            //Initialize the model and view models.
            model = new MyModel(new MyTelnetClient());
            vm = new ViewModel(this.model);
            this.mvm = new ManualViewModel(this.model);
            this.dvm = new DashBoardViewModel(this.model);
            this.pvm = new PlaneViewModel(this.model);
            //binding context.
            DataContext = vm;

        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            FlightMainPage main = new FlightMainPage(model);
            //bind context.
            main.dashboard.DataContext = this.dvm;
            main.map.DataContext = this.pvm;
            main.joystick.DataContext = this.mvm;
            main.sliders.DataContext = this.mvm;

            vm.VM_Ip = ipText.Text;
            //Check whether entered values are valid and correct.
            int val;
            if (portText.Text == "" || ipText.Text == "")
            {
                MessageBox.Show("You need to fill the fields!");
            }

            else if (!int.TryParse(portText.Text, out val))
            {
                MessageBox.Show("You need to fill a valid numeric port value!");
            }
            //If everything is ok, we try to start the connection to the server.
            else
            {
                //set the port.
                vm.VM_Port = int.Parse(portText.Text);

                try
                {
                    this.vm.Connect();
                    main.Show();
                    this.Close();
                }
                catch
                {
                    Console.WriteLine("Error with connection!");
                }
            }
          
        }
        // Exit and close the application.
        private void exitButton_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
