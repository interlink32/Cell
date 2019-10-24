using Connection;
using Dna;
using Dna.usercontact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
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
        public static LiteCollection<r_diff> dbdiff(long user)
        {
            return db.GetCollection<r_diff>("diff_" + user);
        }
        public static LiteCollection<singlevalue> dbsingle => db.GetCollection<singlevalue>();
        public static long index
        {
            get
            {
                var dv = dbsingle.FindOne(i => i.id == nameof(index));
                if (dv == null)
                    return 0;
                return long.Parse(dv.value);
            }
            set
            {
                singlevalue dv = new singlevalue()
                {
                    id = nameof(index),
                    value = value.ToString()
                };
            }
        }
    }
}