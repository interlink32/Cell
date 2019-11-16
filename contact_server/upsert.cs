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
            var contact = dbcontact.FindOne(i => i.partnerid == partnerid);
            bool newcontact = contact == null;
            if (newcontact)
            {
                contact = new r_contact()
                {
                    partnerid = partnerid
                };
            }
            if (ownersetting != null)
                contact.ownersetting = ownersetting.Value;
            if (partnersetting != null)
                contact.partnersetting = partnersetting.Value;
            dbcontact.Upsert(contact);
            if (newcontact)
                setdiff(owner, partnerid, difftype.update);
            setdiff(owner, partnerid, difftype.updatecontact);
            //if (contact.ownersetting == e_contactsetting.none && contact.partnersetting == e_contactsetting.none)
            //{
            //    dbcontact.Delete(i => i.partnerid == contact.partnerid);
            //    setdiff(owner, partnerid, difftype.delete);
            //}
            notify(owner);
        }
        private static void setdiff(long owner, long partner, difftype difftype)
        {
            var dbdiff = s.dbdiff(owner);
            diff.set(dbdiff, partner, difftype);
        }
    }
}