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
    class sum : service_gene<f_sum>
    {
        public async override Task<response> get_answer(f_sum request)
        {
            await Task.Delay(12);
            return new f_sum.done()
            {
                result = request.a + request.b
            };
        }
    }
}