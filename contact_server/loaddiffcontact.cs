using Dna;
using Dna.common;
using Dna.usercontact;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactserver
{
    class loaddiffcontact : myservice<q_loaddiffcontact>
    {
        public async override Task<answer> getanswer(q_loaddiffcontact question)
        {
            await Task.CompletedTask;
            var dblog = s.dbdiff(question.z_user);
            return diff.getdiffcontact(dblog, question.index);
        }
    }
}