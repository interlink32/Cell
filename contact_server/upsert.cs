using Dna;
using Dna.common;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class upsert : myservice<q_upsert>
    {
        public async override Task<answer> getanswer(q_upsert question)
        {
            await Task.CompletedTask;
            var core = dbcore.FindOne(i => i.include(question.z_user, question.partner));
            if (core == null)
            {
                core = new r_core() { memebers = new long[] { question.z_user, question.partner } };
                if (question.mysetting == e_connectionsetting.blocked)
                    core.block(question.z_user, true);
                dbcore.Insert(core);

                var userdb = dbcontact(question.z_user);
                userdb.Insert(new r_contact()
                {
                    contact = core.id,
                    mysetting = question.mysetting,
                    partner = question.partner,
                    partnersetting = e_connectionsetting.none
                });

                var partnerdb = dbcontact(question.partner);
                partnerdb.Insert(new r_contact()
                {
                    contact = core.id,
                    mysetting = e_connectionsetting.none,
                    partner = question.z_user,
                    partnersetting = question.mysetting
                });
                notify(question.partner, new n_new() { contact = core.id });
            }
            else
            {
                var userdb = dbcontact(question.z_user);
                var usercontact = userdb.FindOne(i => i.partner == question.partner);
                usercontact.mysetting = question.mysetting;
                userdb.Update(usercontact);

                var dbpartner = dbcontact(question.partner);
                var partnercontact = dbpartner.FindOne(i => i.partner == question.z_user);
                partnercontact.partnersetting = question.mysetting;
                dbpartner.Update(partnercontact);
                notify(question.partner, new n_update() { contact = core.id });
            }
            return null;
        }
    }
}