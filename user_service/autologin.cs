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
    class autologin : myservice<q_autologin>
    {
        public override async Task<answer> getanswer(q_autologin request)
        {
            await Task.CompletedTask;
            var dv = dbtoken.FindOne(i => i.value == request.token);
            if (dv == null)
                return new q_autologin.invalid_token();
            else
            {
                var user = dbuser.FindOne(i => i.id == dv.user);
                return new q_autologin.done
                {
                    user = user.clone()
                };
            }
        }
    }
}