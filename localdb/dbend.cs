using Connection;
using Dna;
using Dna.profile;
using Dna.usercontact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace localdb
{
    public class dbend<entity, contact> where entity : s_entity where contact : s_contact
    {
        private readonly long receiver;
        protected string uniquename => "dbend_" + receiver;
        public string indexid { get; } = typeof(entity).FullName + typeof(contact).FullName;
        LiteDatabase db;
        public dbend(long receiver)
        {
            db = new LiteDatabase(reference.root(uniquename));
            this.receiver = receiver;
            this.indexid = indexid;
        }
        protected LiteCollection<full> dbfull => db.GetCollection<full>("full" + receiver);
        protected LiteCollection<diffentity> dbdiff => db.GetCollection<diffentity>("diff" + receiver);
        public class full
        {
            public long id { get; set; }
            public entity entity { get; set; }
            public contact contact { get; set; }
        }
    }
}