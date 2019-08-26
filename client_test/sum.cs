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
            var req = request as f_sum;
            var res = response as f_sum.done;
            return req.a + req.b == res.result;
        }
        Random random = new Random();
        public override request get_request()
        {
            return new f_sum()
            {
                a = random.Next(),
                b = random.Next()
            };
        }
    }
}