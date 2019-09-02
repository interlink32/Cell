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
    class login : service_gene<f_login>
    {
        public async override Task<answer> get_answer(f_login request)
        {
            await Task.CompletedTask;
            if (request.userid + "pass" == request.password)
                return new f_login.done() { id = long.Parse(request.userid) };
            else
                return new f_login.invalid();
        }
    }
}