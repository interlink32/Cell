using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;

namespace client_test
{
    class subtraction : gene_tester
    {
        public override bool checking(request request, response response)
        {
            return true;
        }
        Random random = new Random();
        public override request get()
        {
            return new Dna.test.a_subtraction() { a = random.Next(), b = random.Next() };
        }
    }
}