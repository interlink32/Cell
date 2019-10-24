using Connection;
using Dna;
using Dna.common;
using Dna.profile;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class sync
    {
        public sync()
        {
            client.notifyadd(e_chromosome.profile, e_chromosome.contact, run);
        }
        async void run(long obj)
        {
            var dv = await mainserver.q(new q_loadentity(e_chromosome.profile) { index = s.index }) as q_loadentity.doen;
            foreach (var i in dv.updatedentity)
                updateentity(i);
            foreach (var i in dv.deletedentity)
                deleteentity(i);
        }
        private void deleteentity(long deleteditem)
        {
            foreach (var partner in s.dbcontact(deleteditem).FindAll().Select(i => i.partnerid).ToArray())
                deleteentity(partner, deleteditem);
        }
        private void deleteentity(long partner, long deleteditem)
        {
            s.dbcontact(partner).Delete(i => i.partnerid == deleteditem);
            LiteCollection<r_diff> dbdiff = s.dbdiff(partner);
            dbdiff.Delete(k => k.partnerid == deleteditem);
            dbdiff.Insert(new r_diff()
            {
                partnerid = deleteditem,
                diiftype = difftype.contactdeleted
            });
        }
        static void updateentity(long updateditem)
        {
            updateentity(updateditem, updateditem);
            foreach (var partner in s.dbcontact(updateditem).FindAll().Select(k => k.partnerid).ToArray())
                updateentity(partner, updateditem);
        }
        static void updateentity(long partner, long updateditem)
        {
            LiteCollection<r_diff> dbdiff = s.dbdiff(partner);
            dbdiff.Delete(k => k.partnerid == updateditem && k.diiftype == difftype.entityupdate);
            dbdiff.Insert(new r_diff()
            {
                partnerid = updateditem,
                diiftype = difftype.entityupdate
            });
        }
    }
}