using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace core
{
    public class reference
    {
        static bool local = false;
        public static IPAddress validip()
        {
            if (local)
                return localip();
            return IPAddress.Parse("94.182.191.71");
        }
        public static IPAddress localip()
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