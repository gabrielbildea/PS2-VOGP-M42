using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAdress = iphostInfo.AddressList[0];
            IPEndPoint ipEndpoint = new IPEndPoint(ipAdress, 32000);



            try
            {
                while (true)
                {
                    Socket client = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    client.Connect(ipEndpoint);

                    Console.WriteLine("Socket created to {0}", client.RemoteEndPoint.ToString());


                    byte[] data = new byte[10];
                    int m = client.Receive(data);

                    Console.WriteLine("" + Encoding.ASCII.GetString(data));
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
