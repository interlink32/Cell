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
        internal long userid = default;
        private readonly mainserver server;
        Func<question, Task<answer>> get_Answer;
        public responder(mainserver service, TcpClient tcp, byte[] key, Func<question, Task<answer>> get_answer)
        {
            this.mainkey = key;
            this.tcp = tcp;
            this.server = service;
            get_Answer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
        }
        async void reading(object o)
        {
            question question = null;
            try
            {
                if (!(await read() is question q))
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
                        if (server.id == e_chromosome.user)
                            rsv = await get_Answer(dv);
                        else
                            rsv = await mainserver.q(dv);
                        if (userid == 0 && rsv is q_login.done done)
                        {
                            userid = done.user.id;
                            if (dv.notifyconnection)
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
            get_Answer = null;
            tcp.Close();
            tcp = null;
            server.remove(this);
        }
        internal void remove_pulse()
        {
            try
            {
                if (tcp.Available != 0)
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