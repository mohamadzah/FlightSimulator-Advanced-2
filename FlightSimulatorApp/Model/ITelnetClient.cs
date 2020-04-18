using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    //Itelnetclient interface.
    interface ITelnetClient
    {
        //connect to the server.
        void connect(string ip, int port);
        //write a message to the server.
        void write(string command);
        //read back from the server.
        string read();
        //disconnect from server.
        void disconnect();
    }
}
