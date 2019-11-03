using Connection;
using Dna;
using Dna.common;
using LiteDB;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace localdb
{
    public class dbend
    {
        protected LiteDatabase db;
        public readonly long userid;
        protected client client;
        public dbend(long userid)
        {
            this.userid = userid;
            client = new client(userid);
            db = new LiteDatabase(reference.root("dbclient" + userid + ".db"));
        }
    }
    public abstract class dbend<entity, contact> : dbend where entity : s_entity where contact : s_contact
    {
        public string uniquename => "dbend" + typeof(contact).FullName;
        public dbend(long userid) : base(userid)
        {
            
        }
        public class fullentity
        {
            public long id { get; set; }
            public entity entity { get; set; }
            public contact contact { get; set; }
        }
        public LiteCollection<fullentity> dbentity => db.GetCollection<fullentity>("entity" + uniquename);
        internal LiteCollection<diffend> dbdiff => db.GetCollection<diffend>("diff" + uniquename);
        internal LiteCollection<s_index> dbindex => db.GetCollection<s_index>("index" + uniquename);
        internal long index
        {
            get
            {
                return dbindex.FindOne(i => i.id == uniquename)?.index ?? 0;
            }
            set
            {
                dbindex.Upsert(new s_index()
                {
                    id = uniquename,
                    index = value
                });
            }
        }
    }
}