using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.test;

namespace client_test
{
    class multi : gene_tester
    {
        public override bool checking(request request, response response)
        {
            var req = request as f_multiplication;
            var res = response as f_multiplication.done;
            return req.a * req.b == res.result;
        }
        Random random = new Random();
        public override request get_request()
        {
            var gene = new f_multiplication()
            {
                a = random.Next(1000, 2000),
                b = random.Next(1000, 2000)
            };
            return gene;
        }
    }
}