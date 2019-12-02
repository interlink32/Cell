using Connection;
using Dna.common;
using Dna.message;
using LiteDB;
using localdb;
using Newtonsoft.Json;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    static class db
    {
        static LiteDatabase database = new LiteDatabase(reference.root("message.db"));
        static LiteCollection<s_id> dbid => database.GetCollection<s_id>("id");
        static LiteCollection<s_message> dbmain(long owner) => database.GetCollection<s_message>("main" + owner);
        static LiteCollection<diff> dbdiff(long owner) => database.GetCollection<diff>("diff" + owner);
        internal static s_id getid(long messageid)
        {
            return dbid.FindOne(i => i.id == messageid);
        }
        public static long create(long sender)
        {
            s_id dv = new s_id()
            {
                sender = sender,
                time = DateTime.Now
            };
            dbid.Insert(dv);
            return dv.id;
        }
        public static void upsert(long sender, long reciver, s_message message)
        {
            upsert_(sender, reciver, message, e_messagestate.send);
            upsert_(reciver, sender, message, e_messagestate.receive);
        }
        static void upsert_(long owner, long partner, s_message message, e_messagestate messagestate)
        {
            message.partner = partner;
            message.messagestate = messagestate;
            dbmain(owner).Upsert(message);
            diff.set(dbdiff(owner), message.id, difftype.update);
            mainserver.notify(owner);
        }
        public static q_loaddiff.done loaddiff(long owner, long index)
        {
            return diff.loaddiff(dbmain(owner), dbdiff(owner), index);
        }
    }
}