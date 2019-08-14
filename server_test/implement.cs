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
    class implement : server<a_value>
    {
        protected async override Task<response> answer(a_value request)
        {
            await Task.Delay(10);
            a_value.done done = new a_value.done()
            {
                value = request.value + 1
            };
            return done;
        }
    }
}
