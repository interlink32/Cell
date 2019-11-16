using Connection;
using Dna;
using Dna.profile;
using Dna.user;
using Dna.usercontact;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactserver
{
    class sync : synchronizer<s_profile>
    {
        public sync() : base(e_chromosome.profile, (long)e_chromosome.usercontact)
        {
        }
        protected override void apply(s_profile entity)
        {
            var partners = s.dbcontact(entity.id).FindAll().Select(i => i.partnerid).ToArray();
            if (partners.Length == 0)
            {
                var dbcontact = s.dbcontact(entity.id);
                dbcontact.Insert(new r_contact()
                {
                    partnerid = entity.id,
                    ownersetting = e_contactsetting.ordinary,
                    partnersetting = e_contactsetting.ordinary
                });
                var dbdiff = s.dbdiff(entity.id);
                diff.set(dbdiff, entity.id, difftype.updatecontact);
                mainserver.sendnotify(entity.id);
            }
            else
                foreach (var i in partners)
                {
                    var dv = s.dbdiff(i);
                    diff.set(dv, entity.id, difftype.update);
                    mainserver.sendnotify(i);
                }
        }
        protected override void delete(long entity)
        {
            var dv = s.getcontactdiff(entity);
            foreach (var i in dv)
                diff.set(i, entity, difftype.delete);
        }
        protected async override Task<s_profile[]> getentities(long[] ids)
        {
            var dv = await client.question(new q_loadallprofile() { ids = ids }) as q_loadallprofile.done;
            return dv.profiles;
        }
    }
}