using Connection;
using Dna;
using Dna.user;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    abstract class myservice<T> : service<T> where T : question
    {
        static LiteDatabase db = new LiteDatabase(reference.root("userserver.db"));
        public static LiteCollection<r_user> dbuser => db.GetCollection<r_user>();
        public static LiteCollection<r_serverinfo> dbserverinfo => db.GetCollection<r_serverinfo>();
        public static LiteCollection<r_difference> dbdiff => db.GetCollection<r_difference>();
    }
}