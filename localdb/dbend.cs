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
        public readonly long userid;
        protected string dbname => "dbend_" + userid;
        protected string tableid { get; } = typeof(entity).Name + "_" + typeof(contact).Name;
        LiteDatabase db;
        public dbend(long userid)
        {
            this.userid = userid;
            db = new LiteDatabase(reference.root(dbname + ".db"));
        }
        protected LiteCollection<full> dbfull => db.GetCollection<full>("full_" + tableid);
        protected LiteCollection<diff> dbdiff => db.GetCollection<diff>("diff_" + tableid);
        public class full
        {
            public long id { get; set; }
            public entity entity { get; set; }
            public contact contact { get; set; }
        }
    }
}