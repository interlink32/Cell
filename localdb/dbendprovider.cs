using Dna;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class dbendprovider<entity, contact> : dbend<entity, contact> where entity : s_entity where contact : s_contact
    {
        public dbendprovider(long userid) : base(userid)
        {
            dbdiff.Delete(i => true);
        }
        public void upsert(full full)
        {
            var dv = userid;
            dbfull.Upsert(full);
            diff.set(dbdiff, full.id, difftype.update);
        }
        public void delete(long id)
        {
            dbfull.Delete(i => i.id == id);
            diff.set(dbdiff, id, difftype.delete);
        }
        public full load(long id)
        {
            return dbfull.FindOne(i => i.id == id);
        }
    }
}