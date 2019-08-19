using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.test;

namespace client_test
{
    class sum : gene_tester
    {
        public override bool checking(request request, response response)
        {
            var req = request as a_sum;
            var res = response as a_sum.done;
            return req.a + req.b == res.result;
        }
        Random random = new Random();
        public override request get()
        {
            return new a_sum()
            {
                a = random.Next(),
                b = random.Next()
            };
        }
    }
}