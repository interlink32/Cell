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
        private readonly server server;
        Func<question, Task<answer>> get_Answer;
        public responder(server service, TcpClient tcp, byte[] key, Func<question, Task<answer>> get_answer)
        {
            this.mainkey = key;
            this.tcp = tcp;
            this.server = service;
            get_Answer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
        }
        async void reading(object o)
        {
            question req = null;
            try
            {
                if (!(await read() is question q))
                    throw new Exception("fjbhdjbjdjkgndjgjdkvg");
                req = q;
            }
            catch
            {
                close();
                return;
            }
            switch (req)
            {
                case q_setkey dv:
                    {
                        key32 = crypto.Decrypt(dv.key32, mainkey);
                        iv16 = crypto.Decrypt(dv.iv16, mainkey);
                        localwrite(null);
                    }
                    break;
                case q_getchromosome dv:
                    {
                        var res = await get_Answer(req);
                        localwrite(res);
                    }
                    break;
                case q_autologin dv:
                    {
                        if (userid != 0)
                            throw new Exception("lkfjbjfjbjdfhbhcnvc");
                        var res = await get_Answer(req);
                        if (res is q_autologin.done done)
                            userid = done.user.id;
                        localwrite(res);
                    }
                    break;
                case q_serverlogin serverlogin:
                    {
                        if (userid != 0)
                            throw new Exception("kgjdjbjdjbjdjbkfkbjsdk");
                        var res = await get_Answer(req);
                        if (res is q_serverlogin.done done)
                            userid = done.userid;
                        localwrite(res);
                    }
                    break;
                case q_gettoken dv:
                    {
                        if (userid != 0)
                            throw new Exception("lkfojhjfjbjdfhbhcnvc");
                        var res = await get_Answer(req);
                        localwrite(res);
                    }
                    break;
                case q_getuser dv:
                    {
                        var res = await get_Answer(req);
                        localwrite(res);
                    }
                    break;
                case q_logout dv:
                    {
                        var res = await get_Answer(req);
                        localwrite(res);
                    }
                    break;
                case q_indirectlogin dv:
                    {
                        if (userid != 0)
                            throw new Exception("lkfjblseejbjdfhbhcnvc");
                        var rsv = await q.get(new q_getuser()
                        {
                            token = dv.token
                        });
                        switch (rsv)
                        {
                            case q_getuser.done done:
                                {
                                    userid = done.user.id;
                                    localwrite(new q_indirectlogin.done());
                                    if (dv.acceptnotifications)
                                    {
                                        server.add(this);
                                        return;
                                    }
                                }
                                break;
                            case q_getuser.invalidtoken invalidtoken:
                                {
                                    localwrite(new developer_error() { code = "kgknjfkmfmbmfmbmcmd" });
                                    await Task.Delay(1000 * 5);
                                    report.error_e?.Invoke(this, "invalid introcode");
                                }
                                break;
                        }
                    }
                    break;
                case q_sendactivecode dv:
                    {
                        var res = await get_Answer(req);
                        localwrite(res);
                    }
                    break;
                default:
                    {
                        if (userid == 0 || userid == 1)
                            localwrite(new loginrequired());
                        else
                        {
                            req.z_user = userid;
                            var res = await get_Answer(req);
                            localwrite(res);
                        }
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