using Connection;
using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace localdb
{
    public class localdb<entity> where entity : s_entity
    {
        public readonly long userid;
        protected string dbname => "dbend_" + userid;
        protected string tableid { get; } = typeof(entity).Name;
        LiteDatabase db;
        public localdb(long userid)
        {
            this.userid = userid;
            db = new LiteDatabase(reference.root(dbname + ".db"));
        }
        internal LiteCollection<entity> dbmain => db.GetCollection<entity>("full_" + tableid);
        internal LiteCollection<diff> dbdiff => db.GetCollection<diff>("diff_" + tableid);
    }
}