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
        List<server_side> list = new List<server_side>();
        async void listen()
        {
            var tcp = await listener.AcceptTcpClientAsync();
            server_side dv = new server_side(tcp, private_key, get_answer);
            list.Add(dv);
            dv.error_e += Dv_error_e;
            listen();
        }
        private void Dv_error_e(core arg1, string arg2)
        {
            var dv = arg1 as server_side;
            dv.error_e -= Dv_error_e;
            dv.dispose();
            list.Remove(dv);
        }
        async Task<response> get_answer(request request)
        {
            var dv = elementsF.FirstOrDefault(i => i.z_gene == request.z_gene);
            if (dv == null)
                throw new Exception("zpjrughdwifhdksjgkfvhy");
            var dv2 = await dv.z_get_answer(request);
            return dv2;
        }
    }
}