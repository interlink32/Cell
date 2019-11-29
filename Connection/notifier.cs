using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class notifier : clientitem
    {
        public notifier(long userid, string chromosome) : base(userid, chromosome)
        {
            isnotifier = true;
        }
        protected async override Task cycle()
        {
            var dv = await getlen();
            if (dv != pulsenotify)
                throw new Exception("bldfgkkjbjfnjbjgnvjdjbmdfkbk");
            notifyevent(userid, chromosome);
        }
        static SemaphoreSlim nlock = new SemaphoreSlim(1, 1);
        static List<notifyaction> nlist = new List<notifyaction>();
        internal async static void notifyevent(long userid, string chromosome)
        {
            await nlock.WaitAsync();
            var dv = nlist.Where(i => i.user == userid && i.chromosome == chromosome).ToArray();
            foreach (var i in dv)
                i.action(userid);
            nlock.Release();
        }
        public static async void notifyadd(e_chromosome sender, long user, Action<long> action)
        {
            await nlock.WaitAsync();
            nlist.Add(new notifyaction()
            {
                action = action,
                user = user,
                chromosome = sender.ToString()
            });
            nlock.Release();
            addnotifier(user, sender.ToString());
        }
        static List<notifier> nrlist;
        static SemaphoreSlim nrlock = new SemaphoreSlim(1, 1);
        async static void addnotifier(long user, string chromosome)
        {
            bool first = false;
            await nrlock.WaitAsync();
            if (nrlist == null)
            {
                nrlist = new List<notifier>();
                first = true;
            }
            var dv = nrlist.FirstOrDefault(i => i.userid == user && i.chromosome == chromosome);
            if (dv == null)
            {
                dv = new notifier(user, chromosome);
                nrlist.Add(dv);
            }
            nrlock.Release();
            if (first)
                sendpulse();
        }
        public static async void notifyremove(Action<long> action)
        {
            await nlock.WaitAsync();
            var dv = nlist.RemoveAll(i => i.action == action);
            nlock.Release();
        }
        class notifyaction
        {
            public long user = default;
            public Action<long> action = default;
            internal string chromosome = default;
        }
        public static async void sendpulse()
        {
            await nrlock.WaitAsync();
            var dv = nrlist.ToArray();
            nrlock.Release();
            foreach (var i in dv)
                i.sendpalse();
            await Task.Delay(5000);
            sendpulse();
        }
    }
}