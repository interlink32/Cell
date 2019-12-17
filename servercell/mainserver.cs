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
            add(tcp);
            listen();
        }
        async void add(TcpClient tcp)
        {
            try
            {
                byte[] data = new byte[1];
                await tcp.GetStream().ReadAsync(data, 0, data.Length);
                switch ((netid)data[0])
                {
                    case netid.questioner:
                        {
                            responder dv = new responder(this, tcp, privatekey);
                        }
                        break;
                    case netid.notifier:
                        {
                            notifyport dv = new notifyport(this, tcp, privatekey);
                        }
                        break;
                    case netid.clientspeed:
                        {
                            serverspeed dv = new serverspeed(this, tcp, privatekey);
                        }
                        break;
                    default:
                        {
                            tcp.Close();
                        }
                        break;
                }
            }
            catch
            {
                tcp?.Close();
            }
        }
        internal async void remove(notifyport val)
        {
            await locker.WaitAsync();
            list.Remove(val);
            locker.Release();
        }
        internal async Task<answer> getanswer(question request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
        static List<notifyport> list = new List<notifyport>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        internal async void add(notifyport dv)
        {
            await locker.WaitAsync();
            list.Add(dv);
            locker.Release();
        }
        static async Task<notifyport[]> get(long user)
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
                i.notify();
        }
        public static async Task<answer> question(question question)
        {
            return await client.question(serverid, question);
        }
    }
}