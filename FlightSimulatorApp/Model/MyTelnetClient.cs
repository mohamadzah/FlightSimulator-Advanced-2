using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    class MyTelnetClient : ITelnetClient {
        private TcpClient tcp;
        private NetworkStream stream;
        private Mutex mutex = new Mutex();

        public void connect(string ip, int port)
        {
            this.tcp = new TcpClient();
            try
            {
                tcp.Connect(ip, port);
                stream = tcp.GetStream();
                stream.ReadTimeout = 10000;
            }

            catch(Exception)
            {
                Console.WriteLine("Could not connect!");
            }
        }

        public void write(string command)
        {
            Byte[] encodedMsg = Encoding.ASCII.GetBytes(command);
            stream.Write(encodedMsg, 0, encodedMsg.Length);
        }

        public string read()
        {
            Byte[] sentBack = new Byte[256];
            int len = stream.Read(sentBack, 0, sentBack.Length);
            string message = Encoding.ASCII.GetString(sentBack, 0, len);
            return message;
        }

        public void disconnect()
        {
            tcp.Client.Close(); //disconnect from the server.
            Console.WriteLine("Disconnected!");
        }
    }
}
