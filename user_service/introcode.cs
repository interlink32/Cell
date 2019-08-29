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
            public byte[] code = new byte[32];
        }
        static List<item> list = new List<item>();
        internal static SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public static byte[] new_code(long user)
        {
            item item = new item()
            {
                user = user
            };
            random.NextBytes(item.code);
            add(item);
            return item.code;
        }

        private static async void add(item item)
        {
            await locking.WaitAsync();
            list.Add(item);
            locking.Release();
        }

        public static async Task<long> get_userid(byte[] introcode)
        {
            await locking.WaitAsync();
            var dv = list.FirstOrDefault(i => i.code.SequenceEqual(introcode));
            locking.Release();
            return dv.user;
        }
    }
}