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
    class servernotifier
    {
        public long userid { get; private set; }
        private readonly mainserver mainserver;
        private readonly TcpClient tcp;
        private readonly byte[] privatekey;
        public servernotifier(mainserver mainserver, TcpClient tcp, byte[] privatekey)
        {
            runing();
            this.mainserver = mainserver;
            this.tcp = tcp;
            this.privatekey = privatekey;
        }
        async void runing()
        {
            try
            {
                if (userid == 0)
                {
                    byte[] data = new byte[128];
                    await tcp.GetStream().ReadAsync(data, 0, data.Length);
                    data = crypto.Decrypt(data, privatekey);
                    var token = BitConverter.ToInt64(data, 0);
                    var dv = await mainserver.question(new q_login() { token = token }) as q_login.done;
                    if (dv.error_invalid)
                    {
                        tcp.Close();
                        return;
                    }
                    else
                    {
                        userid = dv.userid;
                    }
                }
                tcp.GetStream().WriteByte(netid.connectpulse);
                await Task.Delay(1000 * 4);
                runing();
            }
            catch
            {
                tcp.Close();
                mainserver.remove(this);
            }
        }
        internal void notify()
        {
            try
            {
                tcp.GetStream().WriteByte(netid.newnotify);
            }
            catch
            {
                tcp.Close();
                mainserver.remove(this);
            }
        }
    }
}