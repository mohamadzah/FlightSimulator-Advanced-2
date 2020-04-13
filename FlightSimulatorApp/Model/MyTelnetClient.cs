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
            mutex.WaitOne();
            tcp.GetStream().Write(encodedMsg, 0, encodedMsg.Length);
            tcp.GetStream().Read(sentBack, 0, 256);
            Console.WriteLine(Encoding.ASCII.GetString(sentBack, 0, sentBack.Length));
            mutex.ReleaseMutex();
        }

        public string read()
        {
            byte[] sentBack = new byte[256];
            mutex.WaitOne();
            tcp.GetStream().Read(sentBack, 0, 256);
            string message = Encoding.ASCII.GetString(sentBack, 0, sentBack.Length);
            mutex.ReleaseMutex();
            return message;

        }

        public void disconnect()
        {
            tcp.Client.Close(); //disconnect from the server.
            Console.WriteLine("Disconnected!");
        }
    }
}
