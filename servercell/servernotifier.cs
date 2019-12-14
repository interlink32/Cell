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
        private const int timeout = clientnotifier.timeuot / 2;
        private readonly mainserver mainserver;
        private readonly TcpClient tcp;
        private readonly byte[] privatekey;
        public servernotifier(mainserver mainserver, TcpClient tcp, byte[] privatekey)
        {
            this.mainserver = mainserver;
            this.tcp = tcp;
            this.privatekey = privatekey;
            runing();
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
                        tcp.GetStream().WriteByte(netid.invalidtoken);
                        await Task.Delay(100);
                        tcp.Close();
                        return;
                    }
                    userid = dv.userid;
                    mainserver.add(this);
                    tcp.GetStream().WriteByte(netid.login);
                }
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
                    tcp.GetStream().WriteByte(netid.newnotify);
                else
                    tcp.GetStream().WriteByte(netid.connectpulse);
            }
            catch
            {
                tcp.Close();
                mainserver.remove(this);
            }
        }
    }
}