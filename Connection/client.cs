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
        List<client_side> list;
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public async Task<bool> login(string userid, string password)
        {
            if (!started)
                await start();
            var side = list.First(i => i.chromosome == chromosome.user);
            var dv = await side.question(new f_login()
            {
                userid = userid,
                password = password
            }) as f_login.done;
            logged = dv != null;
            return logged;
        }
        async Task start()
        {
            await locking.WaitAsync();
            if (started)
            {
                locking.Release();
                return;
            }
            list = new List<client_side>();
            var info = reference.get_central_info();
            client_side client = new client_side(chromosome.central, reference.get_endpoint(info.endpoint), info.public_key)
            {
                client = this
            };
            await client.connect();
            list.Add(client);
            var rsv = await client.question(new f_get_chromosome_info()) as f_get_chromosome_info.done;
            foreach (var i in rsv.chromosome_infos)
            {
                client = new client_side(i.chromosome, reference.get_endpoint(i.endpoint), i.public_key) { client = this };
                list.Add(client);
            }
            foreach (var i in list)
                i.notify_e += I_notify_e;
            started = true;
            locking.Release();
        }
        class notify_me
        {
            public Type type = null;
            public Action<notify> action = null;
        }
        List<notify_me> nl = new List<notify_me>();
        SemaphoreSlim nl_locking = new SemaphoreSlim(1, 1);
        public async void add(Type type, Action<notify> action)
        {
            if (action == null || type == null)
                throw new Exception("kgjjdimkgjnkcdknmdkbdjc");
            await nl_locking.WaitAsync();
            nl.Add(new notify_me()
            {
                action = action,
                type = type
            });
            nl_locking.Release();
        }
        async void I_notify_e(notify obj)
        {
            await nl_locking.WaitAsync();
            var dv = nl.Where(i => i.type == obj.GetType()).ToArray();
            nl_locking.Release();
            foreach (var i in dv)
                i.action(obj);
        }
        public bool logged { get; private set; }

        bool started = false;
        public async Task<response> question(request request)
        {
            if (!logged)
                throw new Exception("kdjdhbujfnbidndjbnxjfd");
            var dv = list.First(i => i.chromosome.ToString() == request.z_chromosome);
            var rt = await dv.question(request);
            return rt;
        }
    }
}