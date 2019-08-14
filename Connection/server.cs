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
    public abstract class server<T> : client where T : request
    {
        TcpListener listener = null;
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public void start(IPEndPoint endPoint)
        {
            listener = new TcpListener(endPoint);
            listener.Start();
            accept();
        }
        List<server_base<T>> list = new List<server_base<T>>();
        async void accept()
        {
            var tcp = await listener.AcceptTcpClientAsync();
            var dv = new server_base<T>(tcp, answer);
            await locking.WaitAsync();
            list.Add(dv);
            locking.Release();
        }
        protected abstract Task<response> answer(T request);
        public void notify(long user, string gene)
        {
            locking.WaitAsync();
            var dv = list.Where(i => i.user == user).ToArray();
            locking.Release();
            notify notify = new notify()
            {
                calling = gene
            };
            foreach (var i in dv)
                i.write(notify);
        }
    }
}