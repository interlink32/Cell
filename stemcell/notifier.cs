using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Dna;

namespace stemcell
{
    public class notifier : clientitem
    {
        public notifier(string chromosome, long userid) : base(chromosome, userid)
        {
            isnotifier = true;
            running();
        }
        Action<long> notify = default;
        async void running()
        {
            var dv = await getnotify();
            notify?.Invoke(dv);
            running();
        }

        static List<notifier> list = new List<notifier>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public static async void notifyadd(e_chromosome chromosome, long userid, Action<long> sync)
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
        public static void notifyremove(Action<long> sync)
        {

        }
    }
}