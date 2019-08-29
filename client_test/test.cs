using Connection;
using Dna.user;
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
            new sum(),
            new multi(),
            new subtraction(),
        };

        static int n = 1000;
        static long get_id()
        {
            n++;
            return n;
        }
        public async Task start()
        {
            client client = new client();
            var n = get_id();
            var dv = await client.question(new f_login()
            {
                userid = n.ToString(),
                password = n + "pass"
            }) as f_login.done;
            if (dv == null)
                throw new Exception("lgkdhbjdjbjfbjucjdnbjcbndjbjcxn");
            start(client);
        }
    }
}