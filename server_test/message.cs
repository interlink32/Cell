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
    class message : service_gene<q_message>
    {
        public async override Task<answer> get_answer(q_message request)
        {
            var dv = await notify(new n_message(request.user)
            {
                sender = request.z_user,
                value = request.value
            });
            return new q_message.done() { receive = dv };
        }
    }
}