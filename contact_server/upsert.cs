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
    class upsert : myservice<q_upsertcontact>
    {
        public async override Task<answer> getanswer(q_upsertcontact question)
        {
            await Task.CompletedTask;
            if (question.z_user == question.partner)
                return new developererror() { code = "lbfkbkfjbjfdjvjdjb" };
            create(question.z_user, question.mysetting, question.partner, null);
            create(question.partner, null, question.z_user, question.mysetting);
            return null;
        }
        private static void create(long owner, e_contactsetting? ownersetting, long partnerid, e_contactsetting? partnersetting)
        {
            var dbcontact = s.dbcontact(owner);
            var dv = dbcontact.FindOne(i => i.partnerid == partnerid);
            if (dv == null)
                dv = new r_contact()
                {
                    partnerid = partnerid
                };
            if (ownersetting != null)
                dv.ownersetting = ownersetting.Value;
            if (partnersetting != null)
                dv.partnersetting = partnersetting.Value;
            dbcontact.Upsert(dv);
            var dbdiff = s.dbdiff(owner);
            dbdiff.Delete(i => i.partnerid == partnerid && i.diiftype != difftype.entityupdate);
            dbdiff.Upsert(new r_diff()
            {
                partnerid = partnerid,
                diiftype = difftype.contactupdate
            });
            notify(owner);
        }
    }
}