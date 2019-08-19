using Connection;
using Dna;
using Dna.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client_test
{
    public abstract class gene_tester
    {
        public abstract request get();
        public abstract bool checking(request request, response response);
        client client;
        public void start(client client)
        {
            this.client = client;
            Action();
        }
        List<int> list = new List<int>();
        async void Action()
        {
            var dv = get();
            DateTime start = DateTime.Now;
            var rsv = await client.question(dv);
            var time = DateTime.Now - start;
            list.Add((int)time.TotalMilliseconds);
            if (!checking(dv, rsv))
                throw new Exception("lfpdkjbjdibkdbkdkbmdkn");
            Action();
        }
    }
}