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

namespace user_service
{
    abstract class myservice<T> : service<T> where T : question
    {
        public static dbentity<r_user> db = new dbentity<r_user>(e_chromosome.user);
        public static LiteCollection<r_user> dbuser => db.table;
        public static LiteCollection<r_serverinfo> dbserverinfo => db.db.GetCollection<r_serverinfo>("serverinfo");
    }
}