using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LxChatt
{
    //Av Alexander Mlakar NTI Gymnasiet 2018.
    //Utifrån aritkel från C-Sharpskolan http://csharpskolan.se/article/broadcast-och-chatt
    class Program
    {
        private const int ListenPort = 11000;
        static void Main(string[] args)
        {
        }

        static void Listener()
        {
            UdpClient listener = new UdpClient(ListenPort);

            try
            {
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, ListenPort);
                byte[] bytes = listener.Receive(ref groupEP);
                Console.WriteLine("Recived broadcast from {0} : {1}\n", groupEP.ToString(), Encoding.UTF8.GetString(bytes, 0, bytes.Length));

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
