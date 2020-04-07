using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MyTelnetClient : ITelnetClient {
        private TcpClient tcp;
        private NetworkStream stream;

        public void connect(string ip, int port)
        {
            this.tcp = new TcpClient();
            try
            {
                tcp.Connect(ip, port);
                stream = tcp.GetStream();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Could not connect!");
            }
        }

        public void write(string command)
        {
            byte[] encodeMsg = Encoding.ASCII.GetBytes(command);
        }

        public void disconnect()
        {
            tcp.Client.Close(); //disconnect from the server.
            Console.WriteLine("Disconnected!");
        }
    }
}
