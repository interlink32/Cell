using Dna;
using Dna.user;
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

        //------------------------------------------------------------------------

        static List<questioner> list = new List<questioner>();
        static SemaphoreSlim qlock = new SemaphoreSlim(1, 1);
        static async Task<questioner> get(long user, string chromosome)
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
        public static async void notifyadd(long user, e_chromosome chromosome, Action<long> action)
        {
            await nlock.WaitAsync();
            nlist.Add(new notifyaction()
            {
                action = action,
                user = user
            });
            nlock.Release();
            await get(user, chromosome.ToString());
        }
        public static async void notifyremove(Action<long> action)
        {
            await nlock.WaitAsync();
            var dv = nlist.FirstOrDefault(i => i.action == action);
            nlist.Remove(dv);
            nlock.Release();
        }
        class notifyaction
        {
            public long user = default;
            public Action<long> action = default;
        }
        static SemaphoreSlim nlock = new SemaphoreSlim(1, 1);
        static List<notifyaction> nlist = new List<notifyaction>();
        internal async static void notify(long userid)
        {
            await nlock.WaitAsync();
            var dv = nlist.Where(i => i.user == userid).ToArray();
            foreach (var i in dv)
                i.action(userid);
            nlock.Release();
        }
        public static async Task<T> question<T>(question question, long user = 0) where T : answer
        {
            if (user == 0 && question.z_permission != e_permission.free)
                throw new Exception("kgjdjrbjcnbjfjnfjvbixjbjdkvb");
            var dv = await get(user, question.z_chromosome);
            return await dv.question(question) as T;
        }
        public static async Task<answer> question(question question, long user = 0)
        {
            return await question<answer>(question, user);
        }
        internal static async Task<s_user> getuser(long user)
        {
            var dv = await question<q_loaduser.done>(new q_loaduser() { userid = user });
            return dv?.user;
        }
        internal static async void sendpulse()
        {
            await qlock.WaitAsync();
            var dv = list.ToArray();
            qlock.Release();
            foreach (var i in dv)
                i.sendpalse();
            await Task.Delay(5000);
            sendpulse();
        }
    }
}