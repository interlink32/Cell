using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Connection
{
    public class reference
    {
        internal static IPEndPoint getendpoint(string endPoint)
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
        public static s_chromosome basechromosome => new s_chromosome()
        {
            chromosome = e_chromosome.user,
            endpoint = new IPEndPoint(validip(), 10001).ToString(),
            publickey = resource.user_public_key
        };
        static bool local = true;
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
        public static string root(string name)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Caaa\\" + name;
        }
    }
}