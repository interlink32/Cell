using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class rename : myservice<q_rename>
    {
        public async override Task<answer> getanswer(q_rename question)
        {
            await Task.CompletedTask;
            var dv = dbuser.FindOne(i => i.id == question.user);
            dv.fullname = question.fullname;
            dbuser.Update(dv);
            notify(question.user, new n_rename());
            return null;
        }
    }
}