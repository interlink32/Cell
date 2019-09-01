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
    class send_message : service_gene<f_send_message>
    {
        public async override Task<response> get_answer(f_send_message request)
        {
            var dv = await notify(new n_new_message(request.receiver_user)
            {
                sender = request.z_user,
                message = request.message
            });
            return new f_send_message.done() { resieved = dv };
        }
    }
}