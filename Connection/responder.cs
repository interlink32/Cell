using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class responder : core
    {
        public override string ToString()
        {
            return "ID : " + userid;
        }
        private readonly mainserver server;
        Func<question, Task<answer>> get_Answer;
        public responder(mainserver service, TcpClient tcp, byte[] key, Func<question, Task<answer>> get_answer)
        {
            mainkey = key;
            this.tcp = tcp;
            server = service;
            get_Answer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
            connected = true;
        }
        async void reading(object o)
        {
            question question = null;
            try
            {
                if (!(await servicread() is question q))
                    throw new Exception("fjbhdjbjdjkgndjgjdkvg");
                question = q;
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
                            localwrite(new developererror() { code = "flfbfblflblfgfbndhvhdc" });
                            return;
                        }
                    }
                    break;
                case e_permission.user:
                    {
                        if (userid == 0)
                        {
                            localwrite(new developererror() { code = "gkdjbjbkvmdmvbkvdkv" });
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
                        localwrite(null);
                    }
                    break;
                case q_login dv:
                    {
                        answer rsv = null;
                        if (server.chromosome == e_chromosome.user)
                            rsv = await get_Answer(dv);
                        else
                            rsv = await mainserver.q(dv);
                        if (userid == 0 && rsv is q_login.done done)
                        {
                            userid = done.user.id;
                            server.add(this);
                        }
                        localwrite(rsv);
                    }
                    break;
                default:
                    {
                        question.z_user = userid;
                        var res = await get_Answer(question);
                        localwrite(res);
                    }
                    break;
            }
            ThreadPool.QueueUserWorkItem(reading);
        }
        private void close()
        {
            server.remove(this);
            get_Answer = null;
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
        internal async void localwrite(gene gene)
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