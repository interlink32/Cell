using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Connection
{
    class reference
    {
        static (string chromosome, IPEndPoint ip) create(string chromosome, int port)
        {
            return (chromosome, new IPEndPoint(IPAddress.Any, port));
        }
        public static (string chromosome, IPEndPoint ip)[] get()
        {
            List<(string chromosome, IPEndPoint ip)> l = new List<(string chromosome, IPEndPoint ip)>();
            create("test", 9090);
            return l.ToArray();
        }
    }
}