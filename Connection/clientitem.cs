using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class clientitem : core
    {
        internal s_chromosome info { get; private set; }
        public clientitem(long userid, string chromosome) : base(chromosome)
        {
            base.userid = userid;
            ini();
        }
        async void ini()
        {
            info = await basic.getchromosome(chromosome);
            mainkey = info.publickey;
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
                Console.Beep(2000, 500);
                string dv = e.Message;
                disconnect();
            }
        }
        protected async Task<answer> q(question question)
        {
            await write(question);
            return await clientread() as answer;
        }
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
            if (!(await clientread() is textanswer textanswer))
                throw new Exception("lkdkbjkbkfmbkcskbmdkb");
            if (userid != 0)
            {
                userlogin dv = await getlogin();
                var rsv = await q(new q_login()
                {
                    notifier = isnotifier,
                    token = dv.token
                });
                if (!(rsv is q_login.done))
                {
                    Console.Beep(1000, 500);
                    throw new Exception("bkdkbmfbcmfmbmmbm");
                }
            }
            connected = true;
            if (isnotifier)
                notifier.notifyevent(userid, chromosome);
        }
        async Task<userlogin> getlogin()
        {
            userlogin userlogin = null;
            while (userlogin == null)
            {
                userlogin = s.dbuserlogin.FindOne(i => i.id == userid);
                await Task.Delay(200);
            }
            return userlogin;
        }
        bool closef = false;
        protected bool isnotifier;

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