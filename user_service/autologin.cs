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
    class autologin : service<q_autologin>
    {
        public override async Task<answer> get_answer(q_autologin request)
        {
            var dv = await login.get(request.divice, request.token);
            if (dv == null)
                return new q_autologin.invalid_token();
            else
                return new q_autologin.done() { id = dv.user };
        }
    }
}