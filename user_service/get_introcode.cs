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
    class get_introcode : service_gene<f_get_introcode>
    {
        public async override Task<answer> get_answer(f_get_introcode request)
        {
            await Task.CompletedTask;
            if (request.z_user == 0)
                return new developer_error() { code = "dkvkdfmnkgkbkdvkdkblfdk" };
            else
            {
                var dv = await login.get(request.divce, request.token);
                if (dv == null)
                    return new developer_error() { code = "lkdlbfkhkvkfmbkgvkc" };
                else
                    return new f_get_introcode.done()
                    {
                        introcode = introcode.new_code(request.z_user)
                    };
            }
        }
    }
}