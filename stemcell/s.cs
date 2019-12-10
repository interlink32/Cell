using stemcell;
using Dna.user;
using Dna.userdata;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
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
        internal static async Task<long> gettoken(long userid)
        {
            userlogin userlogin = null;
            userlogin = s.dbuserlogin.FindOne(i => i.id == userid);
            while (userlogin == null)
            {
                userlogin = s.dbuserlogin.FindOne(i => i.id == userid);
                await Task.Delay(200);
            }
            return userlogin.token;
        }
    }
    class userlogin
    {
        public string callerid { get; set; }
        public long id { get; set; }
        public long token { get; set; }
        public bool general { get; set; }
    }
}