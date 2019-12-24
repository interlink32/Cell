using Dna;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servercell
{
    public abstract class servicebase
    {
        public TcpClient tcp { get; internal set; }
        public abstract void start();
        public abstract bool login(byte[] data);
    }
}