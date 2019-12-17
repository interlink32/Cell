using Dna;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servercell
{
    class serverspeed : clientport
    {
        public serverspeed(mainserver mainserver, TcpClient tcp, byte[] privatekey) : base(mainserver, tcp, privatekey, false, false)
        {
        }
        protected async override void start()
        {
            try
            {
                var dv = await receivebyte();
                if (dv != (byte)netid.connectpulse)
                    throw new Exception("lflbkfbjkfjgkjbkfnbfnbj");
                writebyte((byte)netid.connectpulse);
                start();
            }
            catch
            {
                tcp.Close();
            }
        }
    }
}