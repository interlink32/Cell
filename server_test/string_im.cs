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
    class string_im : service_gene<a_sum>
    {
        public async override Task<response> get_answer(a_sum request)
        {
            await Task.Delay(12);
            return new a_sum.done()
            {
                result = request.a + request.b
            };
        }
    }
}