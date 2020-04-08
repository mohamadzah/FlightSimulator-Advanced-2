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
            byte[] sentBack = new byte[256];
            byte[] encodedMsg = Encoding.ASCII.GetBytes(command);
            stream.Write(encodedMsg, 0, encodedMsg.Length);
            stream.Read(sentBack, 0, 256);
            Console.WriteLine(Encoding.ASCII.GetString(sentBack, 0, sentBack.Length));
        }

        public string read()
        {
            byte[] sentBack = new byte[256];
            stream.Read(sentBack, 0, 256);
            string message = Encoding.ASCII.GetString(sentBack, 0, sentBack.Length);
            return message;
        }

        public void disconnect()
        {
            tcp.Client.Close(); //disconnect from the server.
            Console.WriteLine("Disconnected!");
        }
    }
}
