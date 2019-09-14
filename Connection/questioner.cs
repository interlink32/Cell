using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dna;
using Dna.user;

namespace Connection
{
    class questioner : client_item
    {
        public questioner(client client, s_chromosome_info chromosome_Info) : base(client, chromosome_Info)
        {
        }

        protected async override Task cycle()
        {
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
        string ttt = null;
        internal async Task<answer> question(question request)
        {
            DateTime time = DateTime.Now;
            var dv = new request_task()
            {
                question = request
            };
            await locking.WaitAsync();
            list.Add(dv);
            ttt = "ggg";
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