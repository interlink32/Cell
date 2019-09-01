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
    public abstract class service : client
    {
        public abstract service_gene[] elements { get; }
        public abstract byte[] private_key { get; }
        public abstract IPEndPoint endpoint { get; }
        public abstract string userid { get; }
        public abstract string password { get; }

        TcpListener listener;
        public service()
        {
            starting();
        }
        async void starting()
        {
            if (!(userid == "1" || userid == "2"))
            {
                var dv = await login(userid, password);
                if (!dv)
                    throw new Exception("lfjhdhbjdjbjgndjbmdlvkx");
            }
            elementsF = elements;
            foreach (var i in elementsF)
                i.service = this;
            listener = new TcpListener(endpoint);
            listener.Start();
            listen();
        }

        service_gene[] elementsF = null;
        List<server_side> list = new List<server_side>();
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        async void listen()
        {
            var tcp = await listener.AcceptTcpClientAsync();
            server_side dv = new server_side(tcp, private_key, get_answer) { client = this };
            add(dv);
            dv.error_e += Dv_error_e;
            listen();
        }
        private async void Dv_error_e(core arg1, string arg2)
        {
            var dv = arg1 as server_side;
            await locking.WaitAsync();
            list.Remove(dv);
            locking.Release();
            dv.error_e -= Dv_error_e;
            dv.dispose();
        }
        async Task<response> get_answer(request request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
        async void add(server_side dv)
        {
            await locking.WaitAsync();
            list.Add(dv);
            locking.Release();
        }
        async Task<server_side[]> get(long user)
        {
            await locking.WaitAsync();
            var dv = list.Where(i => i.z_user == user).ToArray();
            locking.Release();
            return dv;
        }
        public async void send_notify(notify notify)
        {
            var dv = await get(notify.z_user);
            foreach (var i in dv)
            {
                i.write(notify);
            }
        }
    }
}