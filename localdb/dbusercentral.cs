using Dna;
using Dna.profile;
using Dna.user;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace localdb
{
    public class dbusercentral : dbuniquecenteral<s_profile, s_usercontact>
    {
        public dbusercentral(long userid) : base(userid, e_chromosome.usercontact)
        {
        }
        public override async Task<s_usercontact[]> getcontacts(long[] ids)
        {
            var rsv = await client.question(new q_loadusercontact() { partnerids = ids })
                as q_loadusercontact.done;
            return rsv.usercontacts;
        }
        public async override Task<s_profile[]> getentities(long[] ids)
        {
            var rsv = await client.question(new q_loadallprofile() { ids = ids })
               as q_loadallprofile.done;
            return rsv.profiles;
        }
    }
}