using Dna;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class dbendprovider<entity, contact> : dbend<entity, contact> where entity : s_entity where contact : s_contact
    {
        public dbendprovider(long receiver) : base(receiver)
        {
        }
        public void upsert(full full)
        {
            dbfull.Upsert(full);
            dbdiff.Delete(i => i.itemid == full.id);
            dbdiff.Insert(new diffentity() { itemid = full.id, state = true });
        }
        public void delete(long id)
        {
            dbfull.Delete(i => i.id == id);
            dbdiff.Delete(i => i.itemid == id);
            dbdiff.Insert(new diffentity() { itemid = id });
        }
        public full load(long id)
        {
            return dbfull.FindOne(i => i.id == id);
        }
    }
}