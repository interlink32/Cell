using Dna;
using Dna.central;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class server_side : core
    {
        internal long z_user = 0;
        Func<question, Task<answer>> get_Answer;
        internal client client = null;
        public server_side(TcpClient tcp, byte[] main_key, Func<question, Task<answer>> get_answer) : base(main_key)
        {
            this.tcp = tcp;
            get_Answer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
        }
        internal void dispose()
        {
            get_Answer = null;
            tcp.Dispose();
            tcp = null;
        }
        async void reading(object o)
        {
            if (!(await read() is question req))
                return;
            switch (req)
            {
                case f_set_key dv:
                    {
                        write(null);
                        key32 = crypto.Decrypt(dv.key32, main_key);
                        iv16 = crypto.Decrypt(dv.iv16, main_key);
                    }
                    break;
                case f_login dv:
                    {
                        var res = await get_Answer(req);
                        if (res is f_login.done done)
                            z_user = done.id;
                        write(res);
                    }
                    break;
                case f_set_introcode dv:
                    {

                        var rsv = await client.question(new f_get_userid()
                        {
                            introcode = dv.introcode
                        });
                        switch (rsv)
                        {
                            case f_get_userid.done dv2:
                                {
                                    z_user = dv2.userid;
                                    write(new f_set_introcode.done());
                                }
                                break;
                            case f_get_userid.invalidcode dv2:
                                {
                                    write(new developer_error(""));
                                    await Task.Delay(1000 * 5);
                                    new_error("invalid introcode");
                                }
                                break;
                        }
                    }
                    break;
                case f_get_chromosome_info dv:
                    {
                        var res = await get_Answer(req);
                        write(res);
                    }
                    break;
                default:
                    {
                        if (z_user == 0)
                            write(new login_required());
                        else
                        {
                            req.z_user = z_user;
                            var res = await get_Answer(req);
                            write(res);
                        }
                    }
                    break;
            }
            ThreadPool.QueueUserWorkItem(reading);
        }
    }
}