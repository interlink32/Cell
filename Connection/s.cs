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
        static LiteDatabase db = new LiteDatabase(reference.root("info.db"));
        internal static void save(token_device data)
        {
            var dv = db.GetCollection<token_device>();
            dv.Insert(data);
        }
        public static token_device load(string id)
        {
            return db.GetCollection<token_device>().FindOne(i => i.user_name == id);
        }

        internal static void remove(string user_id)
        {
            db.GetCollection<token_device>().Delete(new BsonValue(user_id));
        }
    }
    public class token_device
    {
        [BsonId]
        public string user_name { get; set; }
        public double device { get; set; }
        public double token { get; set; }
    }
}