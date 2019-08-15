using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Connection
{
    public class reference
    {
        static (string chromosome, IPEndPoint ip) create(string chromosome, IPAddress iPAddress, int port)
        {
            return (chromosome, new IPEndPoint(iPAddress, port));
        }
        internal static (string chromosome, IPEndPoint ip)[] get()
        {
            var dv = GetAny();
            List<(string chromosome, IPEndPoint ip)> l = new List<(string chromosome, IPEndPoint ip)>();
            l.Add(create("test", dv, 9090));
            return l.ToArray();
        }

        public static IPAddress GetAny()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}