using Connection;
using Dna;
using Dna.common;
using Dna.profile;
using Dna.user;
using Dna.usercontact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localdb
{
    public abstract class dbuniquecenteral<entity, contact> : dbunique<entity, contact> where entity : s_entity where contact : s_contact
    {
        public dbuniquecenteral(long userid, e_chromosome chromosome) : base(userid, chromosome)
        {
            dbdiff.Delete(i => true);
            client.notifyadd(chromosome, userid, action);
        }
        async void action(long obj)
        {
            var rsv = await client.question(new q_loaddiffcontact(chromosome) { index = index })
                as q_loaddiffcontact.done;
            if (rsv.updatedentity.Length != 0)
            {
                var entities = await getentities(rsv.updatedentity);
                foreach (var i in entities)
                {
                    fullentity dv = get(i.id, true);
                    dv.entity = i;
                    dbentity.Upsert(dv);
                    setdiff(i.id);
                }
            }
            if (rsv.updatedcontact.Length != 0)
            {
                var contacts = await getcontacts(rsv.updatedcontact);
                foreach (var i in contacts)
                {
                    var dv = get(i.partnerid, false);
                    dv.contact = i;
                    dbentity.Upsert(dv);
                    var dvv = dbentity.FindAll().ToArray();
                    setdiff(i.partnerid);
                }
            }
            index = rsv.currentindex;
        }
        private fullentity get(long id, bool cannull)
        {
            var dv = dbentity.FindOne(i => i.id == id);
            if (dv == null)
                if (cannull)
                    dv = new fullentity()
                    {
                        id = id
                    };
                else
                    throw new Exception("lbkfkbjfjbjcnbdbjcmfl");
            return dv;
        }
        private void setdiff(long id)
        {
            dbdiff.Delete(i => i.itemid == id);
            dbdiff.Insert(new diff()
            {
                itemid = id
            });
        }
        public abstract Task<entity[]> getentities(long[] ids);
        public abstract Task<contact[]> getcontacts(long[] ids);
    }
}