using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketUDP
{
    internal class Server
    {
        static void Main(string[] args)
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.107"),9999);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(iep);

            IPEndPoint RemoteEP = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)RemoteEP;
            byte[] data= new byte[1024];

            int recv = server.ReceiveFrom(data, ref remote);
            string s = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine("Nhan ve tu Client: {0}", s);
            data = Encoding.ASCII.GetBytes("Chao Client");

            server.SendTo(data, remote);

            while (true)
            {
                data = new byte[1024];
                recv = server.ReceiveFrom(data, ref remote);
                s = Encoding.ASCII.GetString(data,0, recv);
                if (s.ToUpper().Equals("QUIT")) break;
                Console.WriteLine(s);
                data = new byte[1024];
                data = Encoding.ASCII.GetBytes(s);
                server.SendTo(data, 0, data.Length, SocketFlags.None,remote);
            }

            server.Close();
        }
    }
}
