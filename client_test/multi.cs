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
            var req = request as a_multiplication;
            var res = response as a_multiplication.done;

            if (req.a * req.b != res.result)
                throw new Exception("");
            return true;
        }
        public override request get()
        {
            var gene = new a_multiplication();
            return gene;
        }
    }
}