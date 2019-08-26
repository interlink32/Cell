using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.test;

namespace client_test
{
    class subtraction : gene_tester
    {
        public override bool checking(request request, response response)
        {
            var req = request as f_subtraction;
            var res = response as f_subtraction.done;
            return req.a - req.b == res.result;
        }
        Random random = new Random();
        public override request get_request()
        {
            return new f_subtraction() { a = random.Next(), b = random.Next() };
        }
    }
}