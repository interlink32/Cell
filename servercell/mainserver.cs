using stemcell;
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

namespace servercell
{
    public abstract class mainserver
    {
        internal static mainserver ds = default;
        public abstract service[] elements { get; }
        public abstract byte[] privatekey { get; }
        public abstract IPEndPoint endpoint { get; }
        public abstract e_chromosome chromosome { get; }
        public abstract string password { get; }

        TcpListener listener;
        static long serverid = default;
        public mainserver()
        {
            if (ds != null)
                throw new Exception("lbkdkbmfkbjdnfdjbncjvndbjn");
            ds = this;
            serverid = (long)chromosome;
            elementsF = elements;
            foreach (var i in elementsF)
                i.server = this;
            listener = new TcpListener(endpoint);
            listener.Start();
            listen();
            login();
        }
        async void login()
        {
            await basic.serverlogin(chromosome, password);
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
            await locker.WaitAsync();
            list.Remove(val);
            locker.Release();
        }
        async Task<answer> getanswer(question request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
        static List<responder> list = new List<responder>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        internal async void add(responder dv)
        {
            await locker.WaitAsync();
            if (!list.Contains(dv))
            {
                list.Add(dv);
            }
            locker.Release();
        }
        static async Task<responder[]> get(long user)
        {
            await locker.WaitAsync();
            var dv = list.Where(i => i.userid == user).ToArray();
            locker.Release();
            return dv;
        }
        public static async void notify(long user)
        {
            var all = await get(user);
            foreach (var i in all)
                i.sendpulse();
        }
        public static async Task<answer> q(question question)
        {
            return await client.question(serverid, question);
        }
    }
}