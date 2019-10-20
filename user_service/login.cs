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
    class login : myservice<q_login>
    {
        public async override Task<answer> getanswer(q_login request)
        {
            await Task.CompletedTask;
            if (!dbdevice.Exists(i => i.id == request.device.id))
                return new q_login.invalid();
            var dv = dbtoken.FindOne(i => i.device == request.device.id && i.token == request.token);
            if (dv == null)
                return new q_login.invalid();
            else
            {
                var user = dbuser.FindOne(i => i.id == dv.user);
                if (user == null)
                    throw new Exception("mvxdjbdjhdhvhxgsbvxnvndmv");
                return new q_login.done() { user = user.clone() };
            }
        }
    }
}