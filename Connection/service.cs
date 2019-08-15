using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class service : client
    {
        public abstract service_element[] elements { get; }
        TcpListener listener;
        public service(IPEndPoint endPoint)
        {
            listener = new TcpListener(endPoint);
            listener.Start();
            listen();
        }
        service_element[] elementsF = null;
        async void listen()
        {
            var dv = await listener.AcceptTcpClientAsync();
            elementsF = elements;
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