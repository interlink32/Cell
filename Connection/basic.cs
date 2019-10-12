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
        static questioner defaultitem = new questioner(0, e_chromosome.user.ToString());
        static async Task<answer> q(question question)
        {
            return await defaultitem.question(question);
        }
        public static async Task logout(long userid)
        {
            var dv = s.dbuserlogin.FindOne(i => i.id == userid);
            if (dv == null)
                return;
            s.dbuserlogin.Delete(i => i.id == userid);
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
                dv = createrandomcode(callerid);
            await q(new q_sendactivecode()
            {
                callerid = callerid,
                randomvalue = dv.value
            });
        }
        private static randomcode createrandomcode(string callerid)
        {
            randomcode dv = new randomcode()
            {
                callerid = callerid,
                value = random.Next()
            };
            s.dbrandom.Upsert(dv);
            return dv;
        }
        public static async Task<bool> login(string callerid, string activecode)
        {
            var userlogin = s.dbuserlogin.FindOne(i => i.callerid == callerid);
            if (userlogin != null)
            {
                await logout(userlogin.id);
                s.dbuserlogin.Delete(i => i.id == userlogin.id);
                return await login(callerid, activecode);
            }
            var code = s.dbrandom.FindOne(i => i.callerid == callerid);
            if (code == null)
                return false;
            var dv = await q(new q_getusertoken()
            {
                callerid = callerid,
                activecode = activecode,
                randomvalue = code.value
            }) as q_getusertoken.done;
            if (dv == null)
                return false;
            s.dbuserlogin.Upsert(new userlogin()
            {
                token = dv.token,
                general = true,
                id = dv.user.id,
                fullname = dv.user.fullname
            });
            s.dbrandom.Delete(i => i.callerid == callerid);
            return true;
        }
        public static async Task<bool> serverlogin(e_chromosome chromosome, string password)
        {
            var dv = await q(new q_getservertoken()
            {
                chromosome = chromosome,
                password = password
            }) as q_getservertoken.done;
            if (dv == null)
                return false;
            s.dbuserlogin.Upsert(new userlogin()
            {
                token = dv.token,
                general = false,
                id = (int)chromosome
            });
            return true;
        }
        public static async Task<s_chromosome> getchromosome(string chromosome)
        {
            if (chromosome == e_chromosome.user.ToString())
                return reference.userchromosome;
            return (await allchromosome()).First(i => i.chromosome.ToString() == chromosome);
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
        public static userinfo[] alluser()
        {
            return s.dbuserlogin.Find(i => i.general).Select(i => i.clone()).ToArray();
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
                if (!users.Any(j => j.id == i.id))
                {
                    users.Add(i);
                    useref?.Invoke((i, true));
                }
            }
            foreach (var i in users.ToArray())
            {
                if (!newlist.Any(j => j.id == i.id))
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
        public long id { get; internal set; }
        public string fullname { get; internal set; }
        public override string ToString() => fullname;
        public override int GetHashCode()
        {
            return 0;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var dv = obj as userinfo;
            return id == dv.id;
        }
    }
}