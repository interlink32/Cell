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
            var dv = await read() as request;
            var dv2 = await get_Answer(dv);
            write(dv2);
            reading();
        }
    }
}