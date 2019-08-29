﻿using Dna;
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
        Func<request, Task<response>> get_Answer;
        internal client client = null;
        public server_side(TcpClient tcp, byte[] main_key, Func<request, Task<response>> get_answer) : base(main_key)
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
            if (!(await read() is request req))
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
                                    write(null);
                                }
                                break;
                            case f_get_userid.invalidcode dv2:
                                {
                                    write(new f_get_introcode.login_required());
                                    await Task.Delay(1000 * 5);
                                    new_error("invalid introcode");
                                }
                                break;
                        }
                    }
                    break;
                default:
                    {
                        var res = await get_Answer(req);
                        write(res);
                    }
                    break;
            }
            ThreadPool.QueueUserWorkItem(reading);
        }
    }
}