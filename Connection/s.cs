using Dna.user;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class s
    {
        static Func<string> get = () =>
        {
            return "lhflbfknlmfblvbvkbmvfb";
        };
        static LiteDatabase db = new LiteDatabase(new ConnectionString()
        {
            Filename = reference.root("info.db"),
            Password = get()
        });
        internal static LiteCollection<userlogin> dbuserlogin => db.GetCollection<userlogin>();
        internal static LiteCollection<randomcode> dbrandom => db.GetCollection<randomcode>();
    }
    class userlogin
    {
        public long id { get; set; }
        public string callerid { get; set; }
        public string fullname { get; set; }
        public string token { get; set; }
        public bool general { get; set; }
        public userinfo clone()
        {
            return new userinfo()
            {
                id = id,
                fullname = fullname
            };
        }
    }
}