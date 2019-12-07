using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dna;
using Dna.common;
using Dna.user;

namespace Connection
{
    class questioner : clientitem
    {
        public questioner(long userid, string chromosome) : base(userid, chromosome)
        {
        }
        protected override async Task cycle()
        {
            if (list.Count > 0)
            {
                var item = await getitem();
                var dv = await q(item.question);
                item.rt.SetResult(dv);
                await remove_item();
            }
        }
        async Task<requesttask> getitem()
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
        List<requesttask> list = new List<requesttask>();
        class requesttask
        {
            public question question = null;
            public TaskCompletionSource<answer> rt = new TaskCompletionSource<answer>();
            public override string ToString()
            {
                return question?.z_chromosome + "." + question?.z_gene;
            }
        }

        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        internal async Task<answer> question(question request)
        {
            DateTime time = DateTime.Now;
            var dv = new requesttask()
            {
                question = request
            };
            await locking.WaitAsync();
            list.Add(dv);
            locking.Release();
            var rsv = await dv.rt.Task;
            if (rsv is error error)
                throw new Exception(error.code);
            var space = DateTime.Now - time;
            return rsv;
        }
    }
}