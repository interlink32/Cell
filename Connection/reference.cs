using Dna;
using Dna.central;
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
        public static IPEndPoint get_endpoint(string endPoint)
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
        internal static s_chromosome_info get_central_chromosome_info()
        {
            return new s_chromosome_info()
            {
                chromosome = chromosome.central,
                endpoint = new IPEndPoint(valid_ip(), 10000).ToString(),
                public_key = Resource1.public_key
            };
        }
        static bool local = true;
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