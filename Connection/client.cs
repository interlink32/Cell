using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        public long userid { get; }
        public client(long userid)
        {
            this.userid = userid;
        }
        public async Task<answer> question(question question)
        {
            return await client.question(question, userid);
        }

        static List<questioner> list = new List<questioner>();
        static SemaphoreSlim qlock = new SemaphoreSlim(1, 1);
        static async Task<questioner> getquestioner(long user, string chromosome)
        {
            await qlock.WaitAsync();
            var dv = list.FirstOrDefault(i => i.userid == user && i.chromosome == chromosome);
            if (dv == null)
            {
                dv = new questioner(user, chromosome);
                list.Add(dv);
            }
            qlock.Release();
            return dv;
        }
        public static async Task<answer> question(question question, long user = 0)
        {
            if (user == 0 && question.z_permission != e_permission.free)
                throw new Exception("kgjdjrbjcnbjfjnfjvbixjbjdkvb");
            var dv = await getquestioner(user, question.z_chromosome);
            return await dv.question(question);
        }
        internal static async void close(notifier notifier)
        {
            await nlock.WaitAsync();
            notifier.close();
            nlist.Remove(notifier);
            nlock.Release();
        }
        static List<notifier> nlist = new List<notifier>();
        public static async void add<T>(long user, Action<T> action) where T : notify, new()
        {
            T d = new T();
            notifier dv = await getn(user, d.z_chromosome);
            dv.add(action);
        }
        public static async void remove<T>(long user, Action<T> action) where T : notify, new()
        {
            T d = new T();
            notifier dv = await getn(user, d.z_chromosome);
            dv.remove<T>(action);
        }

        static SemaphoreSlim nlock = new SemaphoreSlim(1, 1);
        async static Task<notifier> getn(long user, string chromosome)
        {
            await nlock.WaitAsync();
            var dv = nlist.FirstOrDefault(i => i.userid == user && i.chromosome == chromosome);
            if (dv == null)
            {
                dv = new notifier(user, chromosome);
                nlist.Add(dv);
            }
            nlock.Release();
            return dv;
        }
        public static async void reconnect(long user, e_chromosome chromosome, Action action)
        {
            var dv = await getn(user, chromosome.ToString());
            dv.reconnect_e += action;
        }
        internal static async void sendpulse()
        {
            await nlock.WaitAsync();
            foreach (var i in nlist)
                i.sendpulse();
            nlock.Release();
            await Task.Delay(5000);
            sendpulse();
        }
    }
}