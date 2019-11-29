using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Dna.userdata;

namespace Connection
{
    public class client
    {
        public long userid { get; }
        public client(long userid)
        {
            this.userid = userid;
        }
        static bool start = false;
        public async Task<answer> question(question question)
        {
            return await client.question(userid, question);
        }

        //------------------------------------------------------------------------

        static List<questioner> list = new List<questioner>();
        static SemaphoreSlim qlock = new SemaphoreSlim(1, 1);
        static async Task<questioner> get(long user, string chromosome)
        {
            await qlock.WaitAsync();
            if (!start)
            {
                start = true;
                sendpulse();
            }
            var dv = list.FirstOrDefault(i => i.userid == user && i.chromosome == chromosome);
            if (dv == null)
            {
                dv = new questioner(user, chromosome);
                list.Add(dv);
            }
            qlock.Release();
            return dv;
        }
        public async static void close(long userid)
        {
            await qlock.WaitAsync();
            var dv = list.Where(i => i.userid == userid).ToArray();
            foreach (var i in dv)
            {
                i.close();
                list.Remove(i);
            }
            qlock.Release();
        }
        public static async void notifyadd(e_chromosome sender, long receiver, Action<long> action)
        {
            await nlock.WaitAsync();
            nlist.Add(new notifyaction()
            {
                action = action,
                user = receiver,
                chromosome = sender.ToString()
            });
            nlock.Release();
            await get(receiver, sender.ToString());
        }
        public static void notifyadd(e_chromosome sender, e_chromosome receiver, Action<long> action)
        {
            notifyadd(sender, (long)receiver, action);
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
        static SemaphoreSlim nlock = new SemaphoreSlim(1, 1);
        static List<notifyaction> nlist = new List<notifyaction>();
        internal async static void notify(long userid, string chromosome)
        {
            if (userid == 3)
            {

            }
            await nlock.WaitAsync();
            var dv = nlist.Where(i => i.user == userid && i.chromosome == chromosome).ToArray();
            foreach (var i in dv)
                i.action(userid);
            nlock.Release();
        }
        public static async Task<T> question<T>(question question, long user = 0) where T : answer
        {
            if (user == 0 && question.z_permission != e_permission.free)
                throw new Exception("kgjdjrbjcnbjfjnfjvbixjbjdkvb");
            string chromosome = question.z_redirect == null ? question.z_chromosome : question.z_redirect.ToString();
            var dv = await get(user, chromosome);
            return await dv.question(question) as T;
        }
        public static async Task<answer> question(long user, question question)
        {
            return await question<answer>(question, user);
        }
        public static async void sendpulse()
        {
            await qlock.WaitAsync();
            var dv = list.ToArray();
            qlock.Release();
            foreach (var i in dv)
                i.sendpalse();
            await Task.Delay(5000);
            sendpulse();
        }
        public static long[] getalluser()
        {
            return s.dbuserlogin.Find(i => i.general).Select(i => i.id).ToArray();
        }
    }
}