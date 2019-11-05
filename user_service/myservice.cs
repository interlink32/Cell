using Connection;
using Dna;
using Dna.user;
using LiteDB;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class s
    {
        public static dbentity<r_user> db = new dbentity<r_user>("user.db");
        public static LiteCollection<r_serverinfo> dbserverinfo => db.GetCollection<r_serverinfo>("serverinfo");
    }
    abstract class myservice<T> : service<T> where T : question
    {
        public static dbentity<r_user> db => s.db;
        public static LiteCollection<r_serverinfo> dbserverinfo => s.dbserverinfo;
    }
}