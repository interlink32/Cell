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
            create(question.z_user, question.partner, question.mysetting, null);
            create(question.partner, question.z_user, null, question.mysetting);
            return null;
        }
        private static void create(long owner, long partnerid, e_contactsetting? mysetting, e_contactsetting? partnersetting)
        {
            var dbcontact = s.dbcontact(owner);
            var dv = dbcontact.FindOne(i => i.partnerid == partnerid);
            if (dv == null)
                dv = new r_contact()
                {
                    partnerid = partnerid,
                };
            if (mysetting != null)
                dv.mysetting = mysetting.Value;
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
        }
    }
}