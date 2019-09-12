using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client_test
{
    class client_pool
    {
        static async Task<client_tester> create(long id)
        {
            client_tester dv = new client_tester();
            await dv.start(id);
            return dv;
        }

        int index = 1000;
        int max = 1000 + 1;
        client_tester last;
        public async void start(object o)
        {
            await Task.Delay(100);
            if (last == null)
            {
                last = await create(index);
                index++;
            }
            var dv =await create(index);
            last.connect(index);
            last = dv;
            if (index == max)
                return;
            index++;
            ThreadPool.QueueUserWorkItem(start);
        }
    }
}