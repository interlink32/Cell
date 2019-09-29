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
        internal double device = default;
        internal double token = default;
        private readonly server server;
        Func<question, Task<answer>> get_Answer;
        public responder(server service, TcpClient tcp, byte[] key, Func<question, Task<answer>> get_answer)
        {
            this.main_key = key;
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
                case q_set_key dv:
                    {
                        key32 = crypto.Decrypt(dv.key32, main_key);
                        iv16 = crypto.Decrypt(dv.iv16, main_key);
                        local_write(null);
                    }
                    break;
                case q_get_chromosome_info dv:
                    {
                        var res = await get_Answer(req);
                        local_write(res);
                    }
                    break;
                case q_autologin dv:
                    {
                        if (userid != 0)
                            throw new Exception("lkfjbjfjbjdfhbhcnvc");
                        var res = await get_Answer(req);
                        if (res is q_autologin.done done)
                        {
                            userid = done.id;
                            device = dv.device;
                        }
                        local_write(res);
                    }
                    break;
                case q_login dv:
                    {
                        if (userid != 0)
                            throw new Exception("lkfojhjfjbjdfhbhcnvc");
                        var res = await get_Answer(req);
                        if (res is q_login.done done)
                        {
                            userid = done.id;
                            device = done.device;
                        }
                        local_write(res);
                    }
                    break;
                case q_intrologin dv:
                    {
                        if (userid != 0)
                            throw new Exception("lkfjblseejbjdfhbhcnvc");
                        var rsv = await server.question(new q_introcheck()
                        {
                            introcode = dv.introcode
                        });
                        switch (rsv)
                        {
                            case q_introcheck.done dv2:
                                {
                                    userid = dv2.userid;
                                    device = dv.device;
                                    local_write(new q_intrologin.done());
                                    if (dv.accept_notifications)
                                    {
                                        server.add(this);
                                        return;
                                    }
                                }
                                break;
                            case q_introcheck.invalidcode dv2:
                                {
                                    local_write(new developer_error() { code = "kgknjfkmfmbmfmbmcmd" });
                                    await Task.Delay(1000 * 5);
                                    report.error_e?.Invoke(this, "invalid introcode");
                                }
                                break;
                        }
                    }
                    break;
                default:
                    {
                        if (userid == 0)
                            local_write(new login_required());
                        else
                        {
                            req.z_user = userid;
                            var res = await get_Answer(req);
                            local_write(res);
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
        internal async void local_write(gene gene)
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