using Connection;
using Dna;
using Dna.profile;
using Dna.usercontact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class dbend<entity, contact> where entity : s_entity where contact : s_contact
    {
        LiteDatabase db;
        private readonly long receiver;
        public dbend(long receiver)
        {
            db = new LiteDatabase(reference.root(uniquename));
            this.receiver = receiver;
        }
        public LiteCollection<full> dbfull => db.GetCollection<full>("full" + receiver);
        protected LiteCollection<diffentity> dbdiff => db.GetCollection<diffentity>("diff" + receiver);
        public class full
        {
            public long id { get; set; }
            public entity entity { get; set; }
            public contact contact { get; set; }
        }
        protected string uniquename => "dbend_" + receiver;
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
    }
}