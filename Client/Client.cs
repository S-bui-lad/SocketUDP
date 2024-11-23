using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.107"), 9999);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            String s = "Chao Server";
            byte[] data = new byte[1024];
            
            data = Encoding.ASCII.GetBytes(s);
            client.SendTo(data, iep);
            EndPoint remote = (EndPoint)iep;
            data = new byte[1024];
            int recv = client.ReceiveFrom(data, ref remote);
            s = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine("Nhan ve tu Server{0}", s);

            while (true)
            {
                s = Console.ReadLine();
                data = new byte[1024];
                data = Encoding.ASCII.GetBytes(s);
                client.SendTo(data, remote);
                if (s.ToUpper().Equals("QUIT")) break;
                data = new byte[1024];
                recv = client.ReceiveFrom(data, ref remote);
                s = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(s);
            }
            client.Close();
        }
    }
}
