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
        public Home()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).vm;
            (Application.Current as App).main.dashboard.DataContext = (Application.Current as App).boardViewModel;
            (Application.Current as App).main.map.DataContext = (Application.Current as App).planeViewModel;
            (Application.Current as App).main.controls.DataContext = (Application.Current as App).manualViewModel;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).vm.VM_Ip = ipText.Text;
            //Check whether entered values are valid and correct.
            int val;
            if (portText.Text == "" || ipText.Text == "")
            {
                MessageBox.Show("You need to fill the fields with valid values!");
            }

            else if (!int.TryParse(portText.Text, out val))
            {
                MessageBox.Show("You need to fill a valid numeric port value!");
            }

            //If everything is ok, we try to start the connection to the server.
            else
            {
                //set the port.
                (Application.Current as App).vm.VM_Port = int.Parse(portText.Text);
                // try connection.
                try
                {
                    (Application.Current as App).vm.Connect();
                    if ((Application.Current as App).vm.VM_Error != "initiateERR")
                    {
                        (Application.Current as App).main.Show();
                        this.Close();
                    }

                    else
                    {
                        (Application.Current as App).vm.VM_Error = "RESET";
                        MessageBox.Show("Connection request was not established! Please confirm that your input is correct/ your server is up and running!");
                    }
                }

                catch (Exception)
                {
                    (Application.Current as App).vm.VM_Error = "RESET";
                    throw new Exception();
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
