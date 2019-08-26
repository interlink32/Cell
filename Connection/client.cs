using Converter;
using Dna;
using Dna.central;
using Dna.common;
using Dna.test;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        public event Action<string> notify_e;
        List<client_side> list;
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        async Task start()
        {
            await locking.WaitAsync();
            if (started)
            {
                locking.Release();
                return;
            }
            list = new List<client_side>();
            var dv = reference.get_central_chromosome_info();
            client_side client = new client_side(dv.chromosome, reference.get_endpoint(dv.endpoint), dv.public_key);
            await client.connect();
            list.Add(client);
            var rsv = await client.question(new f_get_chromosome_info()) as f_get_chromosome_info.done;
            foreach (var i in rsv.chromosome_infos)
            {
                client = new client_side(i.chromosome, reference.get_endpoint(i.endpoint), i.public_key);
                list.Add(client);
            }
            foreach (var i in list)
                i.notify_e += I_notify_e;
            started = true;
            locking.Release();
        }
        private void I_notify_e(string obj)
        {
            notify_e?.Invoke(obj);
        }
        bool started = false;
        public async Task<response> question(request request)
        {
            if (!started)
                await start();
            var dv = list.First(i => i.chromosome.ToString() == request.z_chromosome);
            var rt= await dv.question(request);
            return rt;
        }
    }
}