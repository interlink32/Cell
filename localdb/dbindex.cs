using stemcell;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class dbindex
    {
        static LiteDatabase db = new LiteDatabase(reference.root("indexdb.db"));
        static LiteCollection<s_index> table => db.GetCollection<s_index>();
        public static long get(string id)
        {
            return table.FindOne(i => i.id == id)?.index ?? 0;
        }
        public static void set(string id, long index)
        {
            table.Upsert(new s_index()
            {
                id = id,
                index = index
            });
        }
    }
}