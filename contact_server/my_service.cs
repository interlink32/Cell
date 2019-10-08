using Connection;
using Dna;
using Dna.contact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    abstract class my_service<T> : service<T> where T : question
    {
        static LiteDatabase db = new LiteDatabase(reference.root("contact.db"));
        public LiteCollection<r_contact> dbcontact(long user)
        {
            return db.GetCollection<r_contact>("contact_" + user);
        }
        public LiteCollection<r_core> dbcore => db.GetCollection<r_core>();
    }
}