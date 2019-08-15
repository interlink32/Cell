using Converter;
using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        public event Action<string> notify_e;
        List<client_base> list = new List<client_base>();
        public client()
        {
            client_base cb;
            var dv = reference.get();
            foreach (var i in dv)
            {
                cb = new client_base(i.chromosome, i.ip);
                cb.notify_e += Cb_notify_e;
                list.Add(cb);
            }
        }
        private void Cb_notify_e(string obj)
        {
            notify_e?.Invoke(obj);
        }
        public async Task<response> question(request request)
        {
            var dv = list.First(i => i.chromosome == request.z_chromosome);
            return await dv.question(request);
        }
    }
}