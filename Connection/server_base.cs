using Dna;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    class server_base<T> : core where T : request
    {
        public long user = 0;
        private readonly Func<T, Task<response>> func;
        public server_base(TcpClient tcp, Func<T, Task<response>> func)
        {
            this.tcp = tcp;
            this.func = func;
            reading();
        }
        async void reading()
        {
            var req = await read();
            var dv = await func(req as T);
            write(dv);
            reading();
        }

    }
}