using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connection;
using Dna;
using Dna.test;

namespace server_test
{
    class multi : service_gene<a_multiplication>
    {
        public async override Task<response> get_answer(a_multiplication request)
        {
            await Task.Delay(10);
            return new a_multiplication.done()
            {
                result = request.a * request.b
            };
        }
    }
}