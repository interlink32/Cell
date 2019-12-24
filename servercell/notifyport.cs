using Dna;
using Dna.user;
using stemcell;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servercell
{
    class notifyport : clientport
    {
        private const int timeout = notifier.timeuot;
        public notifyport(mainserver mainserver, TcpClient tcp, byte[] privatekey) : base(mainserver, tcp, privatekey, false)
        {
            
        }
        protected override void start()
        {
            mainserver.add(this);
            runing();
        }
        private async void runing()
        {
            try
            {
                await Task.Delay(timeout);
                notify(false);
                runing();
            }
            catch
            {
                tcp.Close();
                mainserver.remove(this);
            }
        }
        internal void notify(bool newnotify = true)
        {
            try
            {
                if (newnotify)
                    tcp.GetStream().WriteByte((byte)byteid.newnotify);
                else
                    tcp.GetStream().WriteByte((byte)byteid.connectpulse);
            }
            catch
            {
                tcp.Close();
                mainserver.remove(this);
            }
        }
    }
}