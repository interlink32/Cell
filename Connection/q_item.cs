using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class q_item : core
    {
        internal readonly s_chromosome_info info;
        q_center client = null;
        public override string ToString()
        {
            return "client side: " + info.chromosome.ToString();
        }
        public q_item(q_center client, s_chromosome_info chromosome_Info)
        {
            main_key = chromosome_Info.public_key;
            this.client = client;
            info = chromosome_Info;
            run_cycle();
        }
        public async Task<answer> q(question question)
        {
            await write(question);
            return await read() as answer;
        }
        async Task<request_task> get_item()
        {
            await locking.WaitAsync();
            var dv = list[0];
            locking.Release();
            return dv;
        }
        async Task remove_item()
        {
            await locking.WaitAsync();
            list.Remove(list[0]);
            locking.Release();
        }
        async void cycle(object o)
        {
            if (start)
            {
                await connect();
                if (list.Count > 0)
                {
                    var item = await get_item();
                    try
                    {

                        var dv = await q(item.question);
                        item.rt.SetResult(dv);
                        await remove_item();
                    }
                    catch
                    {
                        connected = false;
                        key32 = iv16 = null;
                    }
                }
            }
            run_cycle();
        }
        private async void run_cycle()
        {
            await Task.Delay(10);
            ThreadPool.QueueUserWorkItem(cycle);
        }

        bool connected = false;
        internal async Task connect()
        {
            if (connected)
                return;
            tcp = new TcpClient();
            var endpoint = reference.get_endpoint(info.endpoint);
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            var keys = crypto.create_symmetrical_keys();
            await write(new q_set_key()
            {
                key32 = crypto.Encrypt(keys.key32, main_key),
                iv16 = crypto.Encrypt(keys.iv16, main_key)
            });
            key32 = keys.key32;
            iv16 = keys.iv16;
            if (!(await read() is void_answer))
                throw new Exception("lkdkbjkbkfmbkcskbmdkb");
            await client.login_side(this);
            connected = true;
        }

        List<request_task> list = new List<request_task>();
        class request_task
        {
            public question question = null;
            public TaskCompletionSource<answer> rt = new TaskCompletionSource<answer>();
            public override string ToString()
            {
                return question?.z_chromosome + "." + question?.z_gene;
            }
        }

        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public bool start = false;
        internal async Task<answer> question(question request)
        {
            start = true;
            DateTime time = DateTime.Now;
            var dv = new request_task()
            {
                question = request
            };
            await locking.WaitAsync();
            list.Add(dv);
            locking.Release();
            var rsv = await dv.rt.Task;
            var space = DateTime.Now - time;
            ThreadPool.QueueUserWorkItem((o) =>
            {
                report.answer_time_e?.Invoke((long)space.TotalMilliseconds, request.z_chromosome);
            });
            report.cunter++;
            return rsv;
        }
    }
}