using Dna;
using Dna.common;
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
        Func<request, Task<response>> get_Answer;
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
            if (req is f_set_key dv)
            {
                write(null);
                key32 = crypto.Decrypt(dv.key32, main_key);
                iv16 = crypto.Decrypt(dv.iv16, main_key);
            }
            else
            {
                var res = await get_Answer(req);
                write(res);
            }
            ThreadPool.QueueUserWorkItem(reading);
        }
    }
}