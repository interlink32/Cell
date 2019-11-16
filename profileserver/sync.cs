using Connection;
using Dna;
using Dna.user;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class sync : synchronizer<s_user>
    {
        internal static dbentity<r_profile> dbentity = new dbentity<r_profile>("profile.db");
        public sync() : base(e_chromosome.user, (long)e_chromosome.profile)
        {
        }
        protected override void apply(s_user entity)
        {
            var dv = dbentity.load(entity.id);
            if (dv == null)
                dv = new r_profile()
                {
                    id = entity.id
                };
            dv.fullname = entity.fullname;
            dbentity.upsert(dv);
            mainserver.sendnotify(e_chromosome.usercontact);
        }
        protected override void delete(long entity)
        {
            dbentity.delete(entity);
            mainserver.sendnotify(e_chromosome.usercontact);
        }
        protected async override Task<s_user[]> getentities(long[] ids)
        {
            var dv = await client.question(new q_loadalluser() { ids = ids }) as q_loadalluser.done;
            return dv.users;
        }
    }
}