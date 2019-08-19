using Dna;
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
        public server_side(TcpClient tcp, Func<request, Task<response>> get_answer)
        {
            this.tcp = tcp;
            reading();
            get_Answer = get_answer;
        }
        async void reading()
        {
            request req;
            try
            {
                req = await read() as request;
            }
            catch
            {
                return;
            }
            var res = await get_Answer(req);
            try
            {
                write(res);
            }
            catch
            {
                return;
            }
            reading();
        }
    }
}