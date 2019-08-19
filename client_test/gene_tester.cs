using Connection;
using Dna;
using Dna.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client_test
{
    public abstract class gene_tester
    {
        public abstract request get();
        public abstract bool checking(request request, response response);
        client client;
        public void start(client client)
        {
            this.client = client;
            Action();
        }
        static SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        async void Action()
        {
            var dv = get();
            DateTime start = DateTime.Now;
            var rsv = await client.question(dv);
            var time = DateTime.Now - start; ;
            if (!checking(dv, rsv))
                throw new Exception("lfpdkjbjdibkdbkdkbmdkn");
            await locking.WaitAsync();
            int tot = (int)time.TotalMilliseconds;
            report.list.Add(tot);
            report.counter++;
            maxf = Math.Max(maxf, tot);
            minf = Math.Min(minf, tot);
            if (report.counter % 200 == 0)
                reporting();
            locking.Release();
            Action();
        }

        public static event Action report_e = null;
        static int maxf = 0;
        static int minf = int.MaxValue;
        static void reporting()
        {
            report.max = maxf;
            report.min = minf;
            maxf = 0;
            minf = int.MaxValue;
            report.avrage = report.list.Sum(i => i) / report.list.Count;
            report.list.Clear();
            report_e?.Invoke();
        }
    }
}