using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Connection;

namespace servercell
{
    class responder : core
    {
        public override string ToString()
        {
            return "ID : " + userid;
        }
        private readonly mainserver mainserver;
        Func<question, Task<answer>> getanswer;
        public responder(mainserver mainserver, TcpClient tcp, byte[] key, Func<question, Task<answer>> get_answer) : base(mainserver.chromosome.ToString())
        {
            mainkey = key;
            this.tcp = tcp;
            this.mainserver = mainserver;
            getanswer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
            connected = true;
        }
        internal async Task<gene> serverread()
        {
            int len = await getlen();
            if (len == pulseconnect)
                return await serverread();
            else
                return await readgene(len);
        }
        async void reading(object o)
        {
            question question = null;
            try
            {
                if (!(await serverread() is question q))
                    throw new Exception("fjbhdjbjdjkgndjgjdkvg");
                question = q;
                question.z_normalize();
            }
            catch
            {
                close();
                return;
            }
            switch (question.z_permission)
            {
                case e_permission.server:
                    {
                        if (userid > 100)
                        {
                            trywrite(new error() { code = "flfbfblflblfgfbndhvhdc" });
                            return;
                        }
                    }
                    break;
                case e_permission.user:
                    {
                        if (userid == 0)
                        {
                            trywrite(new error() { code = "gkdjbjbkvmdmvbkvdkv" });
                            return;
                        }
                    }
                    break;
            }
            switch (question)
            {
                case q_setkey dv:
                    {
                        key32 = crypto.Decrypt(dv.key32, mainkey);
                        iv16 = crypto.Decrypt(dv.iv16, mainkey);
                        trywrite(new textanswer());
                    }
                    break;
                case q_login dv:
                    {
                        answer rsv = null;
                        if (mainserver.chromosome == e_chromosome.user)
                            rsv = await getanswer(dv);
                        else
                            rsv = await mainserver.q(dv);
                        if (userid == 0 && rsv is q_login.done done)
                        {
                            // set user id by server
                            userid = done.user.id;
                            if (dv.notifier)
                                mainserver.add(this);
                        }
                        trywrite(rsv);
                    }
                    break;
                default:
                    {
                        question.z_user = userid;
                        var res = await getanswer(question);
                        trywrite(res);
                    }
                    break;
            }
            ThreadPool.QueueUserWorkItem(reading);
        }
        private void close()
        {
            mainserver.remove(this);
            getanswer = null;
            tcp?.Close();
            tcp = null;
        }
        internal void removepulse()
        {
            try
            {
                if (tcp.Available > 0)
                {
                    byte[] buffer = new byte[tcp.Available];
                    tcp.GetStream().Read(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                close();
            }
        }
        internal async void trywrite(gene gene)
        {
            try
            {
                await write(gene);
            }
            catch
            {
                close();
            }
        }
    }
}