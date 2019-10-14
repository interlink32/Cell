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
    public abstract class mainserver
    {
        public abstract service[] elements { get; }
        public abstract byte[] privatekey { get; }
        public abstract IPEndPoint endpoint { get; }
        public abstract e_chromosome id { get; }
        public abstract string password { get; }

        TcpListener listener;
        static long serverid = default;
        public mainserver()
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
            await notifylock.WaitAsync();
            notifylist.Remove(val);
            notifylock.Release();
        }
        async Task<answer> getanswer(question request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
        List<responder> notifylist = new List<responder>();
        SemaphoreSlim notifylock = new SemaphoreSlim(1, 1);
        internal async void addnotify(responder dv)
        {
            await notifylock.WaitAsync();
            if (!notifylist.Contains(dv))
                notifylist.Add(dv);
            notifylock.Release();
        }
        async Task<responder[]> getnotify(long user)
        {
            await notifylock.WaitAsync();
            var dv = notifylist.Where(i => i.userid == user).ToArray();
            notifylock.Release();
            return dv;
        }
        async void removepuls()
        {
            await notifylock.WaitAsync();
            foreach (var i in notifylist)
                i.remove_pulse();
            notifylock.Release();
            await Task.Delay(5 * 1000);
            removepuls();
        }
        public async void sendnotify(long receiver, notify notify)
        {
            notify.z_receiver = receiver;
            var dv = await getnotify(receiver);
            foreach (var i in dv)
                i.localwrite(notify);
        }
        public static async Task<answer> q(question question)
        {
            return await client.question(question, serverid);
        }
    }
}