using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_test
{
    class client_pool
    {
        private readonly int n;
        public client_pool(int n)
        {
            this.n = n;
            start();
        }
        public async void start()
        {
            test tester;
            for (int i = 0; i < n; i++)
            {
                tester = new test();
                await Task.Delay(100);
            }
        }
    }
}