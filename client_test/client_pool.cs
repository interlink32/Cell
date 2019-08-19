using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_test
{
    class client_pool
    {
        public async void start()
        {
            test tester;
            for (int i = 0; i < 10; i++)
            {
                tester = new test();
                await Task.Delay(100);
            }
        }
    }
}