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
        public static async Task notifyadd(long user, e_chromosome chromosome, Action action)
        {
            var dv = await get(user, chromosome.ToString());
            dv.pulse_e += action;
            dv.reconnect_e += action;
        }
        public static async Task notifyremove(long user, e_chromosome chromosome, Action action)
        {
            var dv = await get(user, chromosome.ToString());
            dv.pulse_e -= action;
            dv.reconnect_e -= action;
        }
        public static async Task<answer> question(question question, long user = 0)
        {
            if (user == 0 && question.z_permission != e_permission.free)
                throw new Exception("kgjdjrbjcnbjfjnfjvbixjbjdkvb");
            var dv = await get(user, question.z_chromosome);
            return await dv.question(question);
        }
        public static async void reconnect(long user, e_chromosome chromosome, Action action)
        {
            var dv = await get(user, chromosome.ToString());
            dv.reconnect_e += action;
        }
        internal static async void sendpulse()
        {
            await qlock.WaitAsync();
            var dv = list.ToArray();
            qlock.Release();
            foreach (var i in dv)
                i.sendnotify();
            await Task.Delay(5000);
            sendpulse();
        }
    }
}