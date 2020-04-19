using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace FlightSimulatorApp.Model
{
    //MyTelnetClient that implements Itelnetclient interface.
    class MyTelnetClient : ITelnetClient {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private Mutex mutex = new Mutex();
        
        //Connecting to the server.
        public void connect(string ip, int port)
        {
            //Initialize the tcpClient.
            this.tcpClient = new TcpClient();
            //Try to establish a connection to the server.
            try
            {
                tcpClient.Connect(ip, port);
                stream = tcpClient.GetStream();
            }

            //Catch an exception in case establishing doesn't work.
            catch(IOException)
            {
                Console.WriteLine("Could not connect!");
                throw new IOException();              
            }
        }

        //Send a message to the server.
        public void write(string command)
        {
            Byte[] encodedMsg = Encoding.ASCII.GetBytes(command);
            //try to send the message to the server.
            try
            {
                tcpClient.ReceiveTimeout = 10000;
                tcpClient.GetStream().Write(encodedMsg, 0, encodedMsg.Length);
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
            try
            {
                int len = tcpClient.GetStream().Read(sentBack, 0, sentBack.Length);
                string message = Encoding.ASCII.GetString(sentBack, 0, len);
                return message;
            }
            catch (IOException)
            {
                Console.WriteLine("The server didn't respond during time frame set");
                disconnect();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Close the established connection
        public void disconnect()
        {
            tcpClient.Client.Close(); //disconnect from the server.
            Console.WriteLine("Disconnected!");
        }
    }
}
