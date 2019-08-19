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
            var dv = valid_ip();
            List<(string chromosome, IPEndPoint ip)> l = new List<(string chromosome, IPEndPoint ip)>();
            l.Add(create("test", dv, 10000));
            return l.ToArray();
        }
        static bool local = false;
        public static IPAddress valid_ip()
        {
            if (local)
                return local_ip();
            return IPAddress.Parse("31.184.135.138");
        }
        public static IPAddress local_ip()
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