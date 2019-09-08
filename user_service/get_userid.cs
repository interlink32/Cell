using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class get_userid : service_gene<f_introcheck>
    {
        public async override Task<answer> get_answer(f_introcheck request)
        {
            var dv = await introcode.get_userid(request.introcode);
            if (dv == 0)
                return new f_introcheck.invalidcode();
            else
                return new f_introcheck.done() { userid = dv };
        }
    }
}