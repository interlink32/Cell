using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace Dna.user
{
    public class s_chromosome
    {
        public e_chromosome chromosome = 0;
        public string endpoint = null;
        public byte[] publickey = null;
        public override string ToString()
        {
            return chromosome.ToString() + " , " + endpoint;
        }
        public IPEndPoint getendpoint
        {
            get
            {
                string[] ep = endpoint.Split(':');
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
        }
    }
}