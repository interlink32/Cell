using Connection;
using Dna;
using Dna.profile;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    abstract class myservice<T> : service<T> where T : question
    {
        static LiteDatabase db = new LiteDatabase(reference.root("profileserver.db"));
        public static LiteCollection<r_profile> dbprofile => db.GetCollection<r_profile>();
    }
}