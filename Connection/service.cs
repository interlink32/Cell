using Dna;
using Dna.common;
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

        TcpListener listener;
        public service()
        {
            elementsF = elements;
            listener = new TcpListener(endpoint);
            listener.Start();
            listen();
        }
        service_gene[] elementsF = null;
        async void listen()
        {
            var dv = await listener.AcceptTcpClientAsync();
            server_side dv2 = new server_side(dv, private_key, get_answer);
            listen();
        }
        async Task<response> get_answer(request request)
        {
            TaskCompletionSource<response> rt = new TaskCompletionSource<response>();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                run(request, rt);
            });
            return await rt.Task;
        }
        private async void run(request request, TaskCompletionSource<response> rt)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            rt.SetResult(dv2);
        }
    }
}