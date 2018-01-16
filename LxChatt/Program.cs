using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LxChatt
{
    //Av Alexander Mlakar NTI Gymnasiet 2018..
    //Utifrån aritkel från C-Sharpskolan http://csharpskolan.se/article/broadcast-och-chatt
    class Program
    {
        private const int ListenPort = 11000;
        static void Main(string[] args)
        {

            //skapa och starta en tråd som körs samtidigt med resten av programmet.
            var listenerThred = new Thread(Listener);
            listenerThred.Start();

            //skapa en socket anslutning för att kunna skicka meddeladen.
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.EnableBroadcast = true;
            IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast, ListenPort);
            Thread.Sleep(1000);

            while (true)
            {
                Console.Write(">");
                string msg = Console.ReadLine();
                byte[] sendbuf = Encoding.UTF8.GetBytes(msg);
                socket.SendTo(sendbuf, ep);
            }
        }
        
        static void Listener()
        {
            UdpClient listener = new UdpClient(ListenPort);

            try
            {
                while (true) {
                    IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("192.168.81.55"), ListenPort);


                    Console.ForegroundColor = ConsoleColor.Green;
                    byte[] bytes = listener.Receive(ref groupEP);
                    Console.WriteLine("Recived broadcast from {0} : {1}\n", groupEP.ToString(), DateTime.Now + " " + Encoding.UTF8.GetString(bytes, 0, bytes.Length));
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            finally
            {

            }
        }
    }
}
