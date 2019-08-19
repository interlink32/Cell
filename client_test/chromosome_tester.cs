using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_test
{
    public abstract class chromosome_tester
    {
        public abstract gene_tester[] gene_testers { get; }
        gene_tester[] gene_testersF = null;
        public void start(client client)
        {
            gene_testersF = this.gene_testers;
            foreach (var i in gene_testersF)
                i.start(client);
        }
    }
}