using Connection;
using Dna;
using Dna.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server_test
{
    class sum : service_gene<q_sum>
    {
        public override async Task<answer> get_answer(q_sum request)
        {
            await Task.CompletedTask;
            return new q_sum.done()
            {
                result = request.a + request.b
            };
        }
    }
}