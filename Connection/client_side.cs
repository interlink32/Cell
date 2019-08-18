﻿using Dna;
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
    class client_side : core
    {
        internal readonly string chromosome;
        private readonly IPEndPoint endPoint;
        public client_side(string chromosome, IPEndPoint endPoint)
        {
            this.chromosome = chromosome;
            this.endPoint = endPoint;
        }
        public async Task connect()
        {
            tcp = new TcpClient();
            await tcp.ConnectAsync(endPoint.Address, endPoint.Port);
            reading();
        }
        public event Action<string> notify_e;
        request_task sent = null;
        async void reading()
        {
            var dv = await read();
            if (dv is notify)
            {
                var dv2 = dv as notify;
                notify_e?.Invoke(dv2.calling);
            }
            else
            {
                await locking.WaitAsync();
                if (sent == null)
                    throw new Exception("lgjfjbjdfjbhhfhvhc");
                sent.task.SetResult(dv as response);
                sent = null;
                send();
                locking.Release();
            }
            reading();
        }
        class request_task
        {
            public request request = null;
            public TaskCompletionSource<response> task = new TaskCompletionSource<response>();
        }
        List<request_task> list = new List<request_task>();
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public async Task<response> question(request request)
        {
            var dv = new request_task()
            {
                request = request,
            };
            await locking.WaitAsync();
            if (tcp == null)
                await connect();
            list.Add(dv);
            send();
            locking.Release();
            return await dv.task.Task;
        }
        void send()
        {
            if (sent != null || list.Count == 0)
                return;
            sent = list.First();
            list.Remove(sent);
            write(sent.request);
        }
    }
}