using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    public class timeout
    {
        private readonly int time;
        private readonly Action expired;
        public timeout(int time, Action expired)
        {
            this.time = time;
            this.expired = expired;
        }
        int n = 0;
        public async void start()
        {
            var dv = n;
            await Task.Delay(time);
            if (dv == n)
            {
                n++;
                expired();
            }
        }
        public void end()
        {
            n++;
        }
    }
}