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
    class getuser : myservice<q_getuser>
    {
        public async override Task<answer> getanswer(q_getuser request)
        {
            await Task.CompletedTask;
            var dv = dbtoken.FindOne(i => i.value == request.token);
            if (dv == null)
                return new q_getuser.invalidtoken();
            else
                return new q_getuser.done() { user = dbuser.FindOne(i => i.id == dv.user).clone() };
        }
    }
}