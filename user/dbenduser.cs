using Connection;
using Dna;
using Dna.profile;
using Dna.user;
using Dna.usercontact;
using localdb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace user
{
    public class dbenduser : synchronizer<s_profile, s_usercontact>
    {
        dbendprovider<s_profile, s_usercontact> dbend;
        public dbenduser(long receiver) : base(e_chromosome.usercontact, receiver)
        {
            dbend = new dbendprovider<s_profile, s_usercontact>(receiver);
        }
        protected override string indexid => nameof(dbenduser) + user;
        protected override void apply(s_usercontact contact)
        {
            var dv = dbend.load(contact.partnerid);
            if (dv == null)
            {
                dv = new dbend<s_profile, s_usercontact>.full();
                dv.id = contact.partnerid;
            }
            dv.contact = contact;
            dbend.upsert(dv);
        }
        protected override void apply(s_profile entity)
        {
            var dv = dbend.load(entity.id);
            if (dv == null)
            {
                dv = new dbend<s_profile, s_usercontact>.full();
                dv.id = entity.id;
            }
            dv.entity = entity;
            dbend.upsert(dv);
        }
        protected override void delete(long entity)
        {
            dbend.delete(entity);
        }
        protected async override Task<s_usercontact[]> getcontacts(long[] ids)
        {
            var rsv = await client.question(new q_loadusercontact() { partnerids = ids })
                 as q_loadusercontact.done;
            return rsv.usercontacts;
        }
        protected async override Task<s_profile[]> getentities(long[] ids)
        {
            var rsv = await client.question(new q_loadallprofile() { ids = ids })
               as q_loadallprofile.done;
            return rsv.profiles;
        }
    }
}