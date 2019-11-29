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
        public static async Task<answer> question(long user, question question)
        {
            if (user == 0 && question.z_permission != e_permission.free)
                throw new Exception("kgjdjrbjcnbjfjnfjvbixjbjdkvb");
            string chromosome = question.z_redirect == null ? question.z_chromosome : question.z_redirect.ToString();
            var dv = await get(user, chromosome);
            return await dv.question(question) as answer;
        }
        public static long[] getalluser()
        {
            return s.dbuserlogin.Find(i => i.general).Select(i => i.id).ToArray();
        }
    }
}