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
    public class db
    {
        protected static LiteDatabase database = new LiteDatabase(reference.root("localdb.db"));
        public readonly long userid;
        protected readonly e_chromosome chromosome;

        protected client client;
        public db(long userid, e_chromosome chromosome)
        {
            this.chromosome = chromosome;
            this.userid = userid;
            client = new client(userid);
        }
    }
    public abstract class dbunique<entity, contact> : db where entity : s_entity where contact : s_contact
    {
        public readonly string uniquename;
        public dbunique(long userid, e_chromosome chromosome) : base(userid, chromosome)
        {
            uniquename = typeof(entity).FullName;
        }
        protected LiteCollection<entity> dbentity => database.GetCollection<entity>("entity");
        internal LiteCollection<diff> dbentitydiff => database.GetCollection<diff>("diffentity");
        protected LiteCollection<contact> dbcontact => database.GetCollection<contact>("contact" + userid);
        internal LiteCollection<diff> dbcontactdiff => database.GetCollection<diff>("diffcontact" + userid);
        internal LiteCollection<s_index> dbindex => database.GetCollection<s_index>();
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