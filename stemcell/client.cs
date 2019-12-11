using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    public class client
    {
        static List<clientitem> list = new List<clientitem>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        private readonly long userid;
        public client(long userid)
        {
            this.userid = userid;
        }
        public async Task<answer> question(question question)
        {
            return await client.question(userid, question);
        }
        static async Task<clientitem> get(long userid, string chromosome)
        {
            await locker.WaitAsync();
            var dv = list.FirstOrDefault(i => i.chromosome == chromosome && i.userid == userid && !i.inp);
            if (dv == null)
            {
                dv = new clientitem(chromosome, userid);
                list.Add(dv);
            }
            dv.inp = true;
            locker.Release();
            return dv;
        }
        public static long[] getalluser()
        {
            return s.dbuserlogin.Find(i => i.general).Select(i => i.id).ToArray();
        }
        public async static Task<answer> question(long userid, question question)
        {
            var dv = await get(userid, question.z_endchromosome);
            var answer = await dv.question(question);
            dv.inp = false;
            return answer;
        }
        public static async void close(long id)
        {
            await locker.WaitAsync();
            foreach (var i in list.Where(i => i.userid == id).ToArray())
            {
                i.close();
                list.Remove(i);
            }
            locker.Release();
        }
    }
}