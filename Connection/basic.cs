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
    public static class basic
    {
        static questioner defaultitem;
        static SemaphoreSlim defaultlock = new SemaphoreSlim(1, 1);
        static async Task<answer> q(question question)
        {
            await defaultlock.WaitAsync();
            if (defaultitem == null)
                defaultitem = new questioner(null, reference.basechromosome);
            defaultlock.Release();
            return await defaultitem.question(question);
        }
        public static async Task logout(string callerid)
        {
            var dv = s.dbuserinfo.FindOne(i => i.callerid == callerid);
            if (dv == null)
                return;
            s.dbuserinfo.Delete(i => i.callerid == callerid);
            await q(new q_logout()
            {
                token = dv.token
            });
        }
        public static Random random = new Random();
        public static async Task sendactivecode(string callerid)
        {
            var dv = s.dbrandom.FindOne(i => i.callerid == callerid);
            if (dv == null)
            {
                dv = new randomcode()
                {
                    callerid = callerid,
                    value = random.Next()
                };
                s.dbrandom.Insert(dv);
            }
            await q(new q_sendactivecode()
            {
                callerid = callerid,
                randomvalue = dv.value
            });
        }
        public static async Task<bool> login(string callerid, string activecode)
        {
            var code = s.dbrandom.FindOne(i => i.callerid == callerid);
            if (code == null)
                return false;
            var dv = await q(new q_gettoken()
            {
                activecode = activecode,
                callerid = callerid,
                randomvalue = code.value
            }) as q_gettoken.done;
            if (dv == null)
                return false;
            var user = await q(new q_getuser() { token = dv.token }) as q_getuser.done;
            s.dbuserinfo.Upsert(new userinfosec()
            {
                callerid = callerid,
                token = dv.token,
                general = true,
                id = user.user.id,
                fullname = user.user.fullname,
                authentic = user.user.authentic
            });
            s.dbrandom.Delete(i => i.callerid == callerid);
            return true;
        }
        public static async Task<bool> serverlogin(string serverid, string password)
        {
            var dv = await q(new q_serverlogin()
            {
                serverid = serverid,
                password = password
            }) as q_serverlogin.done;
            if (dv == null)
                return false;
            s.dbuserinfo.Upsert(new userinfosec()
            {
                callerid = serverid,
                token = dv.token,
                general = false
            });
            return true;
        }
        public static async Task<s_chromosome> getchromosome(string chromosome)
        {
            return (await allchromosome()).First(i => i.ToString() == chromosome);
        }
        static s_chromosome[] chromosomes = null;
        static SemaphoreSlim getlock = new SemaphoreSlim(1, 1);
        public static async Task<s_chromosome[]> allchromosome()
        {
            await getlock.WaitAsync();
            if (chromosomes == null)
                chromosomes = (await q(new q_getchromosome()) as q_getchromosome.done).items;
            getlock.Release();
            return chromosomes;
        }
        public static async Task updateusers()
        {
            var dv = s.dbuserinfo.Find(i => i.general).Select(i => i.callerid).ToArray();
            if (dv.Length == 0)
                return;
            var rsv = await q(new q_loadalluser()
            {
                callerids_filter = dv
            }) as q_loadalluser.done;
            userinfosec userinfosec;
            foreach (var i in rsv.users)
            {
                userinfosec = s.dbuserinfo.FindOne(j => j.id == i.id);
                userinfosec.fullname = i.fullname;
                s.dbuserinfo.Update(userinfosec);
            }
        }
        public static userinfo[] alluser()
        {
            return s.dbuserinfo.Find(i => i.general).Select(i => i.clone()).ToArray();
        }
        static Action<(userinfo user, bool login)> useref;
        public static event Action<(userinfo user, bool login)> user_e
        {
            add
            {
                useref += value;
                if (users == null)
                {
                    users = new List<userinfo>(alluser());
                    checkusers();
                }
            }
            remove
            {
                useref -= value;
            }
        }

        static List<userinfo> users = null;
        static async void checkusers()
        {
            List<userinfo> newlist = new List<userinfo>(alluser());
            foreach (var i in newlist)
            {
                if (!users.Any(j => j.callerid == i.callerid))
                {
                    users.Add(i);
                    useref?.Invoke((i, true));
                }
            }
            foreach (var i in users.ToArray())
            {
                if (!newlist.Any(j => j.callerid == i.callerid))
                {
                    users.Remove(i);
                    useref?.Invoke((i, false));
                }
            }
            await Task.Delay(200);
            checkusers();
        }
    }
    public class userinfo
    {
        public string callerid { get; internal set; }
        public string fullname { get; internal set; }
        public override string ToString() => fullname;
    }
}