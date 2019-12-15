using Dna;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace servercell
{
    class serverspeed : portbase
    {
        public serverspeed(TcpClient tcp, byte[] privatekey) : base(tcp, privatekey, false)
        {
        }
        protected async override void start()
        {
            try
            {
                var dv = await receivebyte();
                await writebyte(netid.connectpulse);
                start();
            }
            catch
            {
                tcp.Close();
            }
        }
    }
}