using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SocketClientServerEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            
            myProgram.Server();
            
        }

        public void Server()
        {
            //IPAddress myIP = IPAddress.Parse("10.140.66.19");
            
            TcpListener listener = new TcpListener(IPAddress.Loopback, 11000);
            listener.Start();
            while (true)
            {
                Console.WriteLine("Ready");
                Socket handler = listener.AcceptSocket();
                NetworkStream stream = new NetworkStream(handler);
                //TcpClient client = listener.AcceptTcpClient();
                //StreamReader reader = new StreamReader(stream);
                //StreamWriter writer = new StreamWriter(stream);
                //writer.AutoFlush = true;
                if(/*reader.ReadLine() != null*/handler.Connected)
                {
                    
                    Console.WriteLine("Receiving something from ");
                    Console.WriteLine(handler.LocalEndPoint);

                    byte[] buffer = new byte[1024];
                    handler.Receive(buffer);
                    string stringResponse = Encoding.ASCII.GetString(buffer);
                    Console.WriteLine(stringResponse);
                    Console.ReadKey();

                    byte[] message = new byte[1024];
                    message  = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
                    handler.Send(message);
                    Console.ReadKey();

                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();
                }




            }
            
        }

        public void Client()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Loopback, 11000));
            byte[] message = new byte[1024];
            message = Encoding.ASCII.GetBytes("Client: Redhead Time ??");
            client.Send(message);

            byte[] response = new byte[1024];
            client.Receive(response);
            string stringResponse = Encoding.ASCII.GetString(response);
            Console.Write("From Server: " + stringResponse);



        }
    }
}
