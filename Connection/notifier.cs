using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class notifier : clientitem
    {
        public notifier(long userid, s_chromosome chromosome_Info) : base(userid, chromosome_Info)
        { }

        public event Action<notify> notify_e;
        protected async override Task cycle()
        {
            var dv = await read() as notify;
            notify_e?.Invoke(dv);
        }
        internal void send()
        {
            try
            {
                if (connected)
                    tcp.GetStream().WriteByte(39);
            }
            catch
            {
                close();
            }
        }
    }
}