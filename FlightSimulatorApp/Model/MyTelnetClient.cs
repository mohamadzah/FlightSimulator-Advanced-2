using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulatorApp.Model
{
    //MyTelnetClient that implements Itelnetclient interface.
    class MyTelnetClient : ITelnetClient {
        private TcpClient tcp;
        private NetworkStream stream;
        private Mutex mutex = new Mutex();
        
        //Connecting to the server.
        public void connect(string ip, int port)
        {
            //Initialize the tcpClient.
            this.tcp = new TcpClient();
            //Try to establish a connection to the server.
            try
            {
                tcp.Connect(ip, port);
                stream = tcp.GetStream();
                stream.ReadTimeout = 10000;
            }
            //Catch an exception in case establishing doesn't work.
            catch(Exception)
            {
                Console.WriteLine("Could not connect!");
            }
        }
        //Send a message to the server.
        public void write(string command)
        {
            Byte[] encodedMsg = Encoding.ASCII.GetBytes(command);
            //try to send the message to the server.
            try
            {
                stream.Write(encodedMsg, 0, encodedMsg.Length);
            }
            catch
            {
                Console.WriteLine();
            }
        }
        //Read back from the server.
        public string read()
        {
            Byte[] sentBack = new Byte[256];
            int len = stream.Read(sentBack, 0, sentBack.Length);
            string message = Encoding.ASCII.GetString(sentBack, 0, len);
            return message;
        }
        //Close the established connection
        public void disconnect()
        {
            tcp.Client.Close(); //disconnect from the server.
            Console.WriteLine("Disconnected!");
        }
    }
}
