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
            dbentitydiff.Delete(i => true);
            dbcontactdiff.Delete(i => true);
            client.notifyadd(chromosome, userid, action);
        }
        async void action(long obj)
        {
            Console.Beep(500, 1000);
            var rsv = await client.question(new q_loaddiffcontact(chromosome) { index = index })
                as q_loaddiffcontact.done;
            if (rsv.updatedentity.Length != 0)
            {
                var entities = await getentities(rsv.updatedentity);
                var check = dbentitydiff.FindAll().ToArray();
                foreach (var i in entities)
                {
                    dbentity.Upsert(i);
                    dbentitydiff.Insert(new diff()
                    {
                        itemid = i.id
                    });
                }
                check = dbentitydiff.FindAll().ToArray();
            }
            if (rsv.updatedcontact.Length != 0)
            {
                var contacts = await getcontacts(rsv.updatedcontact);
                foreach (var i in contacts)
                {
                    dbcontact.Upsert(i);
                    dbcontactdiff.Insert(new diff()
                    {
                        itemid = i.partnerid
                    });
                }
            }
            index = rsv.currentindex;
        }
        public abstract Task<entity[]> getentities(long[] ids);
        public abstract Task<contact[]> getcontacts(long[] ids);
    }
}