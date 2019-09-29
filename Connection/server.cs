using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class server
    {
        public client client = null;
        public async Task<answer> question(question question)
        {
            return await client.question(question);
        }
        public abstract service[] elements { get; }
        public abstract byte[] private_key { get; }
        public abstract IPEndPoint endpoint { get; }
        public abstract string user_name { get; }
        public abstract string password { get; }

        TcpListener listener;
        public server()
        {
            elementsF = elements;
            foreach (var i in elementsF)
                i.server = this;
            listener = new TcpListener(endpoint);
            listener.Start();
            listen();
            create_client();
            remove_puls();
        }
        async void create_client()
        {
            client = new client(user_name, password);
            await client.connect();
        }

        service[] elementsF = null;
        List<responder> list = new List<responder>();
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        async void listen()
        {
            var tcp = await listener.AcceptTcpClientAsync();
            responder dv = new responder(this, tcp, private_key, get_answer);
            listen();
        }
        internal async void remove(responder val)
        {
            await locking.WaitAsync();
            list.Remove(val);
            locking.Release();
        }
        async Task<answer> get_answer(question request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
        internal async void add(responder dv)
        {
            await locking.WaitAsync();
            list.Add(dv);
            locking.Release();
        }
        async Task<responder[]> get(long user)
        {
            await locking.WaitAsync();
            var dv = list.Where(i => i.userid == user).ToArray();
            locking.Release();
            return dv;
        }
        async void remove_puls()
        {
            await locking.WaitAsync();
            foreach (var i in list)
                i.remove_pulse();
            locking.Release();
            await Task.Delay(5 * 1000);
            remove_puls();
        }
        public async void send_notify(long receiver, notify notify)
        {
            var dv = await get(receiver);
            foreach (var i in dv)
            {
                i.local_write(notify);
            }
        }
    }
}