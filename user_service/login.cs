using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace user_service
{
    class login : service<q_login>
    {
        public login()
        {
            load();
        }

        private async static void load()
        {
            var dv = await s.load<item[]>("login");
            if (dv != null)
                list = new List<item>(dv);
        }

        internal class item
        {
            public long user = 0;
            public double divce = 0;
            public double token = 0;
        }
        internal static List<item> list = new List<item>();
        internal static SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        static Random random = new Random();
        internal async static Task<item> get(double divce, double token)
        {
            await locking.WaitAsync();
            var dv = list.FirstOrDefault(i => i.divce == divce && i.token == token);
            locking.Release();
            return dv;
        }
        public async override Task<answer> get_answer(q_login request)
        {
            if (request.userid + "pass" == request.password)
            {
                var id = long.Parse(request.userid);
                await locking.WaitAsync();
                var dv = list.FirstOrDefault(i => i.user == id && i.divce == request.divce);
                locking.Release();
                if (dv == null)
                {
                    dv = new item()
                    {
                        divce = request.divce,
                        token = random.NextDouble(),
                        user = id
                    };
                    await add(dv);
                }
                else
                {
                    dv.token = random.NextDouble();
                    save();
                }
                return new q_login.done()
                {
                    id = id,
                    token = dv.token
                };
            }
            else
                return new q_login.invalid();
        }

        private static async Task add(item dv)
        {
            await locking.WaitAsync();
            list.Add(dv);
            locking.Release();
            save();
        }
        async static void save()
        {
            await locking.WaitAsync();
            s.save("login", list.ToArray());
            locking.Release();
        }
    }
}