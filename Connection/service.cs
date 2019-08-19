using Dna;
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
        TcpListener listener;
        public service(IPEndPoint endPoint)
        {
            elementsF = elements;
            listener = new TcpListener(endPoint);
            listener.Start();
            listen();
        }
        service_gene[] elementsF = null;
        async void listen()
        {
            var dv = await listener.AcceptTcpClientAsync();
            Thread thread = new Thread((o) =>
            {
                server_side dv2 = new server_side(dv, get_answer);
            });
            thread.Start();
            listen();
        }
        async Task<response> get_answer(request request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            return await dv.z_get_answer(request);
        }
    }
}