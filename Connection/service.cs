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
    public abstract class service
    {
        public client client = null;
        public async Task<answer> question(question question)
        {
            return await client.question(question);
        }
        public abstract service_gene[] elements { get; }
        public abstract byte[] private_key { get; }
        public abstract IPEndPoint endpoint { get; }
        public abstract string userid { get; }
        public abstract string password { get; }

        TcpListener listener;
        public service()
        {
            elementsF = elements;
            foreach (var i in elementsF)
                i.service = this;
            listener = new TcpListener(endpoint);
            listener.Start();
            listen();
            create_client();
        }
        void create_client()
        {
            client = new client("ohgrdyvfhvxj");
            client.user_password_e += Client_user_password_e;
        }
        private async Task<(string userid, string password)> Client_user_password_e()
        {
            await Task.CompletedTask;
            return (userid, password);
        }

        service_gene[] elementsF = null;
        List<responder> list = new List<responder>();
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        async void listen()
        {
            var tcp = await listener.AcceptTcpClientAsync();
            responder dv = new responder(this, tcp, get_answer) { main_key = private_key, a = this, q = client };
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
            var dv = list.Where(i => i.z_user == user).ToArray();
            locking.Release();
            return dv;
        }
        public async Task<bool> send_notify(notify notify)
        {
            bool reseve = false;
            var dv = await get(notify.z_receiver);
            foreach (var i in dv)
            {
                await i.write(notify);
                reseve = true;
            }
            return reseve;
        }
    }
}