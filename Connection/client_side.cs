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
    class client_side : core
    {
        internal readonly s_chromosome_info info;
        client client = null;
        public override string ToString()
        {
            return "client side: " + info.chromosome.ToString();
        }
        public client_side(client client, s_chromosome_info chromosome_Info) : base(chromosome_Info.public_key)
        {
            this.client = client;
            this.info = chromosome_Info;
        }
        bool sent = false;
        async void cycle(object o)
        {
            try
            {
                await connect();
                reading();
                if (answer != null)
                    use_answer();
                if (!sent && list.Count > 0)
                {
                    write(list[0].request);
                    sent = true;
                }
                await Task.Delay(10);
                ThreadPool.QueueUserWorkItem(cycle);
            }
            catch
            {
                if (answer != null)
                    use_answer();
                answer = null;
                sent = false;
                reading_inp = false;
                ThreadPool.QueueUserWorkItem(cycle);
            }
        }
        internal async Task connect()
        {
            if (connected)
                return;
            tcp = new TcpClient();
            var endpoint = reference.get_endpoint(info.endpoint);
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            var keys = crypto.create_symmetrical_keys();
            write(new f_set_key()
            {
                key32 = crypto.Encrypt(keys.key32, main_key),
                iv16 = crypto.Encrypt(keys.iv16, main_key)
            });
            key32 = keys.key32;
            iv16 = keys.iv16;
            if (!(await read() is void_answer))
                throw new Exception("lkdkbjkbkfmbkcskbmdkb");
            await client.login(this);
            connected = true;
        }

        List<request_task> list = new List<request_task>();
        class request_task
        {
            public question request = null;
            public TaskCompletionSource<answer> rt = new TaskCompletionSource<answer>();
            public override string ToString()
            {
                return request?.z_chromosome + "." + request?.z_gene;
            }
        }

        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        bool start = false;
        internal async Task<answer> question(question request)
        {
            DateTime time = DateTime.Now;
            var dv = new request_task()
            {
                request = request
            };
            await locking.WaitAsync();
            if (!start)
            {
                start = true;
                ThreadPool.QueueUserWorkItem(cycle);
            }
            list.Add(dv);
            locking.Release();
            var rsv = await dv.rt.Task;
            var space = DateTime.Now - time;
            ThreadPool.QueueUserWorkItem((o) =>
            {
                repotr.answer_time_e?.Invoke((long)space.TotalMilliseconds, request.z_chromosome);
            });
            return rsv;
        }
        public event Action<client_side, notify> notify_e;
        answer answer = null;
        bool reading_inp = false;
        async void reading()
        {
            if (reading_inp)
                return;
            reading_inp = true;
            var dv = await read();
            switch (dv)
            {
                case notify notify:
                    {
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            notify_e?.Invoke(this, notify);
                        });
                    }
                    break;
                case answer answer:
                    {
                        if (this.answer != null)
                            throw new Exception("mogdkejfjcodkgjdkikvxksjg");
                        this.answer = answer;
                    }
                    break;
            }
            reading_inp = false;
        }
        private void use_answer()
        {
            list[0].rt.SetResult(answer);
            answer = null;
            sent = false;
            list.Remove(list[0]);
        }
    }
}