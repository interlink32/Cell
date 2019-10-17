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
    class upsert : myservice<q_createcontact>
    {
        public async override Task<answer> getanswer(q_createcontact question)
        {
            await Task.CompletedTask;
            var core = s.dbcore.FindOne(i => i.include(question.z_user, question.partner));
            if (core == null)
            {
                core = new r_core() { memebers = new long[] { question.z_user, question.partner } };
                s.dbcore.Insert(core);

                var userdb = s.dbcontact(question.z_user);
                userdb.Upsert(new r_contact()
                {
                    id = core.id,
                    partner = question.partner
                });

                var partnerdb = s.dbcontact(question.partner);
                partnerdb.Upsert(new r_contact()
                {
                    id = core.id,
                    partner = question.z_user,
                });
                log.create(core.id, question.z_user, question.partner);
            }
            return null;
        }
    }
}