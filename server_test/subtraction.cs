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
    class subtraction : service_gene<f_subtraction>
    {
        public override async Task<response> get_answer(f_subtraction request)
        {
            await Task.Delay(10);
            return new f_subtraction.done()
            {
                result = request.a - request.b
            };
        }
    }
}