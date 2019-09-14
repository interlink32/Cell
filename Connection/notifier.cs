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
    class notifier : client_item
    {
        public notifier(client client, s_chromosome_info chromosome_Info) : base(client, chromosome_Info)
        {
        }

        protected async override Task cycle()
        {
            var dv = await read() as notify;
            client.notify(dv);
        }
    }
}