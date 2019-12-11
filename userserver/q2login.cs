using stemcell;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2login : myservice<q_login>
    {
        public async override Task<answer> getanswer(q_login request)
        {
            await Task.CompletedTask;
            var dv = db.findone(i => i.token == request.token);
            if (dv == null)
                return new q_login.done() { error_invalid = true };
            else
                return new q_login.done() { userid = dv.id };
        }
    }
}