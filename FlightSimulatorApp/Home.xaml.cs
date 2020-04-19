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
        public ViewModel vm;
        public ManualViewModel manualViewModel;
        public PlaneViewModel planeViewModel;
        DashBoardViewModel boardViewModel;

        public Home()
        {
            InitializeComponent();
            //Initialize the model and view models.
            model = new MyModel(new MyTelnetClient());
            vm = new ViewModel(this.model);
            this.manualViewModel = new ManualViewModel(this.model);
            this.boardViewModel = new DashBoardViewModel(this.model);
            this.planeViewModel = new PlaneViewModel(this.model);
            //binding context.
            DataContext = vm;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            FlightMainPage main = new FlightMainPage(model);
            //bind context.
            main.dashboard.DataContext = this.boardViewModel;
            main.map.DataContext = this.planeViewModel;
            main.controls.DataContext = this.manualViewModel;

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
                // try connection.
                try
                {
                    this.vm.Connect();
                    main.Show();
                    this.Close();
                }
                //catch exception if something went wrong.
                catch
                {
                    Console.WriteLine("Error with connection!");
                }
            }
          
        }

        // Exit and close the application.
        private void exitButton_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
