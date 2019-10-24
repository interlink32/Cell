using Dna;
using Dna.common;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class load : myservice<q_loadusercontact>
    {
        public override async Task<answer> getanswer(q_loadusercontact question)
        {
            await Task.CompletedTask;
            if (question.partnerids?.Length == 0)
                return new developererror() { code = "lglfbkfmknkvmdbkfmdkmvcdkb" };
            var db = s.dbcontact(question.z_user);
            var dv = db.Find(i => question.partnerids.Contains(i.partnerid)).Select(i => i.clone()).ToArray();
            return new q_loadusercontact.done()
            {
                usercontacts = dv
            };
        }
    }
}