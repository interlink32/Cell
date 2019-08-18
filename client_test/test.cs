using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_test
{
    class test : chromosome_tester
    {
        public override gene_tester[] gene_testers => new gene_tester[]
        {
            new multi(),
            new subtraction()
        };
        public test()
        {
            start(new Connection.client());
        }
    }
}