using Connection;
using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace message_server
{
    abstract class my_service<T> : service<T> where T : question
    {
        static LiteDatabase db = new LiteDatabase("d://messanger1.db");

        public LiteCollection<message> db_message(long contact)
        {
            return db.GetCollection<message>("contact" + contact);
        }

        public LiteCollection<situation> db_situation => db.GetCollection<situation>();
    }
}