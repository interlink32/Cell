using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class clientitem : core
    {
        public long userid { get; private set; }
        internal s_chromosome info { get; private set; }
        public clientitem(long id, string chromosome)
        {
            string path = reference.root("");
            Directory.CreateDirectory(path);
            ini(id, chromosome);
            this.chromosome = chromosome;
        }
        async void ini(long userid, string chromosome)
        {
            info = await basic.getchromosome(chromosome);
            mainkey = info.publickey;
            this.userid = userid;
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
                disconnect();
            }
        }
        public async Task<answer> q(question question)
        {
            await write(question);
            return await read() as answer;
        }
        public bool connected { get; private set; }
        public string chromosome { get; }

        bool firstconnect = false;
        public event Action reconnect_e;
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
            if (!(await read() is voidanswer))
                throw new Exception("lkdkbjkbkfmbkcskbmdkb");
            if (userid != 0)
            {
                userlogin dv = await getlogin();
                var rsv = await q(new q_login()
                {
                    notifyconnection = this is notifier,
                    token = dv.token
                });
                if (!(rsv is q_login.done))
                    throw new Exception("bkdkbmfbcmfmbmmbm");
                   // badanswer();
            }
            connected = true;
            if (!firstconnect && this is notifier)
                reconnect_e?.Invoke();
            firstconnect = true;
        }
        async Task<userlogin> getlogin()
        {
            userlogin userlogin = null;
            userlogin = s.dbuserlogin.FindOne(i => i.id == userid);
            while (userlogin == null)
            {
                await Task.Delay(200);
                userlogin = s.dbuserlogin.FindOne(i => i.id == userid);
            }
            return userlogin;
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
            disconnect();
            closef = true;
        }

        private void disconnect()
        {
            tcp.Close();
            connected = false;
            key32 = iv16 = null;
        }
    }
}