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
        public abstract service[] elements { get; }
        public abstract byte[] privatekey { get; }
        public abstract IPEndPoint endpoint { get; }
        public abstract e_chromosome id { get; }
        public abstract string password { get; }

        TcpListener listener;
        static long serverid = default;
        public server()
        {
            serverid = (long)id;
            elementsF = elements;
            foreach (var i in elementsF)
                i.server = this;
            listener = new TcpListener(endpoint);
            listener.Start();
            listen();
            removepuls();
            login();
        }
        async void login()
        {
            if (!s.dbuserlogin.Exists(i => i.id == (int)id))
                await basic.serverlogin(id, password);
        }

        service[] elementsF = null;
        async void listen()
        {
            var tcp = await listener.AcceptTcpClientAsync();
            responder dv = new responder(this, tcp, privatekey, getanswer);
            listen();
        }
        internal async void remove(responder val)
        {
            await locking.WaitAsync();
            mainlist.Remove(val);
            locking.Release();
        }
        async Task<answer> getanswer(question request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
        List<responder> mainlist = new List<responder>();
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        internal async void add(responder dv)
        {
            await locking.WaitAsync();
            if (!mainlist.Contains(dv))
                mainlist.Add(dv);
            locking.Release();
        }
        async Task<responder[]> getmain(long user)
        {
            await locking.WaitAsync();
            var dv = mainlist.Where(i => i.userid == user).ToArray();
            locking.Release();
            return dv;
        }
        async void removepuls()
        {
            await locking.WaitAsync();
            foreach (var i in mainlist)
                i.remove_pulse();
            locking.Release();
            await Task.Delay(5 * 1000);
            removepuls();
        }
        public async void send_notify(long receiver, notify notify)
        {
            notify.z_user = receiver;
            var dv = await getmain(receiver);
            foreach (var i in dv)
                i.localwrite(notify);
        }
        public static async Task<answer> q(question question)
        {
            return await client.question(serverid, question);
        }
    }
}