using Connection;
using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace user_service
{
    class get_introcode : my_server<q_get_introcode>
    {
        Random random = new Random();
        public async override Task<answer> get_answer(q_get_introcode request)
        {
            await Task.CompletedTask;
            if (request.z_user == 0)
                return new developer_error() { code = "dkvkdfmnkgkbkdvkdkblfdk" };
            else
            {
                s_introcode introcode = new s_introcode()
                {
                    user_id = request.z_user,
                    device = request.divce,
                    value = random.NextDouble()
                };
                db_intro.Insert(introcode);
                return new q_get_introcode.done() { introcode = introcode.value };
            }
        }
    }
}