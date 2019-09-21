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
        internal long z_user = 0;
        private readonly server service;
        Func<question, Task<answer>> get_Answer;
        internal server a = null;
        public responder(server service, TcpClient tcp, byte[] key, Func<question, Task<answer>> get_answer)
        {
            this.main_key = key;
            this.tcp = tcp;
            this.service = service;
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
            catch (Exception e)
            {
                var dv = e.Message;
                dv = null;
                get_Answer = null;
                tcp.Close();
                tcp = null;
                a.remove(this);
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
                        if (z_user != 0)
                            throw new Exception("lkfjbjfjbjdfhbhcnvc");
                        var res = await get_Answer(req);
                        if (res is q_autologin.done done)
                        {
                            z_user = done.id;
                            if (dv.accept_notifications)
                                service.add(this);
                        }
                        local_write(res);
                    }
                    break;
                case q_login dv:
                    {
                        if (z_user != 0)
                            throw new Exception("lkfojhjfjbjdfhbhcnvc");
                        var res = await get_Answer(req);
                        if (res is q_login.done done)
                        {
                            z_user = done.id;
                            if (dv.accept_notifications)
                                service.add(this);
                        }
                        local_write(res);
                    }
                    break;
                case q_intrologin dv:
                    {
                        if (z_user != 0)
                            throw new Exception("lkfjblseejbjdfhbhcnvc");
                        var rsv = await service.question(new q_introcheck()
                        {
                            introcode = dv.introcode
                        });
                        switch (rsv)
                        {
                            case q_introcheck.done dv2:
                                {
                                    z_user = dv2.userid;
                                    if (dv.accept_notifications)
                                        service.add(this);
                                    local_write(new q_intrologin.done());
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
                        if (z_user == 0)
                            local_write(new login_required());
                        else
                        {
                            req.z_user = z_user;
                            var res = await get_Answer(req);
                            local_write(res);
                        }
                    }
                    break;
            }
            ThreadPool.QueueUserWorkItem(reading);
        }
        async void local_write(gene gene)
        {
            try
            {
                await write(gene);
            }
            catch { }
        }
    }
}