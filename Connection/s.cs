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
        private static Func<string> get = () =>
        {
            return "lhflbfknlmfblvbvkbmvfb";
        };
        static LiteDatabase db = new LiteDatabase(new ConnectionString()
        {
            Filename = reference.root("info.db"),
            Password = get()
        });
        internal static void save(token_device data)
        {
            LiteCollection<token_device> dv = db_device;
            dv.Insert(data);
        }

        internal static LiteCollection<token_device> db_device => db.GetCollection<token_device>();

        public static token_device load(string user_name)
        {
            return db.GetCollection<token_device>().FindOne(i => i.user_name == user_name);
        }
        internal static void remove(string user_name)
        {
            db.GetCollection<token_device>().Delete(i => i.user_name == user_name);
        }
    }
    public class token_device
    {
        [BsonId]
        public string user_name { get; set; }
        public double device { get; set; }
    }
}