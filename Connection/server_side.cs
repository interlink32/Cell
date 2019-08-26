using Dna;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    class server_side : core
    {
        private readonly Func<request, Task<response>> get_Answer;
        public server_side(TcpClient tcp, byte[] main_key, Func<request, Task<response>> get_answer) : base(main_key)
        {
            this.tcp = tcp;
            get_Answer = get_answer;
            reading();
        }
        async void reading()
        {
            var req = await read() as request;
            if (req == null)
                return;
            if (req is f_set_key)
            {
                var dv = req as f_set_key;
                write(null);
                key32 = await crypto.Decrypt(dv.key32, main_key);
                iv16 = await crypto.Decrypt(dv.iv16, main_key);
            }
            else
            {
                var res = await get_Answer(req);
                write(res);
            }
            reading();
        }
    }
}