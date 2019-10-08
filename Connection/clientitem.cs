using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    abstract class clientitem : core
    {
        internal readonly string callerid;
        internal readonly s_chromosome info;
        public clientitem(string callerid, s_chromosome chromosomeinfo)
        {
            mainkey = chromosomeinfo.publickey;
            this.callerid = callerid;
            info = chromosomeinfo;
            runing();
        }
        protected abstract Task cycle();
        private async void runing()
        {
            if (closef)
                return;
            await Task.Run(trycatch);
            await Task.Delay(10);
            if (closef)
                return;
            runing();
        }
        private async Task trycatch()
        {

            try
            {
                await connect();
                await cycle();
            }
            catch (Exception e)
            {
                Console.Beep();
                string dv = e.Message;
                close();
            }
        }
        public async Task<answer> q(question question)
        {
            await write(question);
            return await read() as answer;
        }
        public bool connected { get; private set; }
        bool firstconnect = false;
        public event Action<clientitem> reconnect_e;
        async Task connect()
        {
            if (connected)
                return;
            tcp = new TcpClient();
            var endpoint = reference.getendpoint(info.endpoint);
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            var keys = crypto.create_symmetrical_keys();
            await write(new q_setkey()
            {
                key32 = crypto.Encrypt(keys.key32, mainkey),
                iv16 = crypto.Encrypt(keys.iv16, mainkey)
            });
            key32 = keys.key32;
            iv16 = keys.iv16;
            if (!(await read() is void_answer))
                throw new Exception("lkdkbjkbkfmbkcskbmdkb");
            if (callerid != null)
            {
                var dv = s.dbuserinfo.FindOne(i => i.callerid == callerid);
                if (dv == null)
                    nullcallerid();
                else
                {
                    var rsv = await q(new q_indirectlogin()
                    {
                        token = dv.token,
                        acceptnotifications = this is notifier
                    });
                    if (!(rsv is q_indirectlogin.done))
                        badanswer();
                }
            }
            connected = true;
            if (!firstconnect && this is notifier)
                reconnect_e?.Invoke(this);
            firstconnect = true;
        }

        async void badanswer()
        {
            await Task.CompletedTask;
            throw new Exception("lvkdlbmfkvkxmkblcc");
        }
        async void nullcallerid()
        {
            await Task.CompletedTask;
            throw new Exception("bkkbkbjfkbjkgdkbdnbjkd");
        }
        bool closef = false;
        internal void close()
        {
            closef = true;
            tcp.Close();
            connected = false;
            key32 = iv16 = null;
        }
    }
}