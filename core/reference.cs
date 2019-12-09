using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Dna.user;

namespace core
{
    public class reference
    {
        static bool local = true;
        public readonly static string serverip = "94.182.191.71";
        public static IPAddress validip()
        {
            if (local)
                return localip();
            return IPAddress.Parse(serverip);
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
        public static IPEndPoint getendpoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }
        static string rootf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Caaa\\";
        public static string root(string name, string subdirectory = null)
        {
            if (subdirectory == null)
                return rootf + @"/" + name;
            else
                return rootf + @"/" + subdirectory + @"/" + name;
        }
    }
}