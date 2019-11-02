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
    public class clientdb
    {
        protected LiteDatabase database;
        public readonly long userid;
        protected readonly e_chromosome chromosome;
        protected client client;
        public clientdb(long userid, e_chromosome chromosome)
        {
            database = new LiteDatabase(reference.root("localdb" + userid + ".db"));
            this.chromosome = chromosome;
            this.userid = userid;
            client = new client(userid);
        }
    }
    public abstract class dbunique<entity, contact> : clientdb where entity : s_entity where contact : s_contact
    {
        public string uniquename => ((long)chromosome).ToString();
        public dbunique(long userid, e_chromosome chromosome) : base(userid, chromosome)
        {

        }
        public class fullentity
        {
            public long id { get; set; }
            public entity entity { get; set; }
            public contact contact { get; set; }
        }
        public LiteCollection<fullentity> dbentity => database.GetCollection<fullentity>("entity" + uniquename);
        internal LiteCollection<diff> dbdiff => database.GetCollection<diff>("diff" + uniquename);
        internal LiteCollection<s_index> dbindex => database.GetCollection<s_index>("index" + uniquename);
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