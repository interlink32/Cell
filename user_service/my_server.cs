using Connection;
using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    abstract class my_server<T> : service<T> where T : question
    {
        static LiteDatabase db = new LiteDatabase(reference.root("server_user.db"));
        public static LiteCollection<s_user> db_user => db.GetCollection<s_user>();
        public LiteCollection<s_device> db_device => db.GetCollection<s_device>();
        public LiteCollection<s_introcode> db_intro => db.GetCollection<s_introcode>();
    }
}