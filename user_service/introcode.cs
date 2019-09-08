using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace user_service
{
    static class introcode
    {
        static Random random = new Random();
        class item
        {
            public long user = 0;
            public double code = 0;
        }
        static List<item> list = new List<item>();
        internal static SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public static double new_code(long user)
        {
            item item = new item()
            {
                user = user,
                code = random.NextDouble()
            };
            add(item);
            return item.code;
        }

        private static async void add(item item)
        {
            await locking.WaitAsync();
            list.Add(item);
            locking.Release();
        }

        public static async Task<long> get_userid(double introcode)
        {
            await locking.WaitAsync();
            var dv = list.FirstOrDefault(i => i.code == introcode);
            locking.Release();
            return dv?.user ?? 0;
        }
    }
}