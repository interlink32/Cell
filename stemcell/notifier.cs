using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dna;

namespace stemcell
{
    public class notifier : clientitem
    {
        public notifier(string chromosome, long userid) : base(chromosome, userid)
        {
            isnotifier = true;
            running();
            pulsing();
        }
        async void pulsing()
        {
            while (true)
            {
                await Task.Delay(5000);
                sendpulse();
            }
        }

        Action notify = default;
        async void running()
        {
            while (true)
            {
                var dv = await getnotify();
                notify?.Invoke();
            }
        }
        public static async void remove(long userid)
        {
            await locker.WaitAsync();
            var dv = list.Where(i => i.userid == userid).ToArray();
            foreach (var i in dv)
            {
                i.close();
                list.Remove(i);
            }
            locker.Release();
        }

        static List<notifier> list = new List<notifier>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public static async void add(e_chromosome chromosome, long userid, Action sync)
        {
            await locker.WaitAsync();
            var dv = list.FirstOrDefault(i => i.chromosome == chromosome.ToString() && i.userid == userid);
            if (dv == null)
            {
                dv = new notifier(chromosome.ToString(), userid);
                list.Add(dv);
            }
            dv.notify += sync;
            locker.Release();
        }
    }
}