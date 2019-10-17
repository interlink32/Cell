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
    class updatecontact : myservice<q_updatecontact>
    {
        public async override Task<answer> getanswer(q_updatecontact question)
        {
            await Task.CompletedTask;
            var db = s.dbcontact(question.z_user);
            var contact = db.FindOne(i => i.id == question.contact);
            if (contact == null)
                return new developererror() { code = "gkdgjjbdnvndbkdmbmdnbb" };
            contact.mysetting = question.mysetting;
            db.Update(contact);

            long partner = contact.partner.id;
            db = s.dbcontact(partner);
            contact = db.FindOne(i => i.id == question.contact);
            if (contact == null)
                throw new Exception("lkdkbmdkbnfnbbchsdnvmd");
            contact.partnersetting = question.mysetting;
            db.Update(contact);

            log.create(question.contact, question.z_user, partner);

            return null;
        }
    }
}