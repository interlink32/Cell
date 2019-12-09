using stemcell;
using Dna;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2upsertcontact : myservice<q_upsertcontact>
    {
        public async override Task<answer> getanswer(q_upsertcontact question)
        {
            await Task.CompletedTask;
            db.upsert(question.z_user, question.partner, question.setting);
            return null;
        }
    }
}