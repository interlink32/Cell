using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace controllibrary
{
    public static class db
    {
        static string uniqappdomain = default;
        public static bool create(string uniqappdomain)
        {
            Mutex app = new Mutex(true, uniqappdomain);
            if (!app.WaitOne(0, false))
                return false;
            db.uniqappdomain = uniqappdomain;
            lite = new LiteDatabase(new ConnectionString()
            {
                Filename = Connection.reference.root(uniqappdomain)
            });
            return true;
        }
        public static LiteDatabase lite { get; private set; }

    }
}