using Connection;
using Dna;
using Dna.profile;
using Dna.usercontact;
using LiteDB;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactserver
{
    abstract class myservice<T> : service<T> where T : question
    {

    }
    static class s
    {
        static LiteDatabase db = new LiteDatabase(reference.root("contact.db"));
        public static LiteCollection<r_contact> dbcontact(long user)
        {
            return db.GetCollection<r_contact>("contact_" + user);
        }
        public static LiteCollection<diff> dbdiff(long user)
        {
            return db.GetCollection<diff>("diff_" + user);
        }
        public static LiteCollection<diff>[] getcontactdiff(long user)
        {
            return dbcontact(user).FindAll().ToArray().Select(i => dbdiff(i.partnerid)).ToArray();
        }
    }
}