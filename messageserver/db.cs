using Connection;
using controllibrary;
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

        static classprovider<database> dbp = new classprovider<database>();
        static async Task<LiteDatabase> getdb(long owner)
        {
            return (await dbp.get(reference.root("db_" + owner + ".db", "message"))).db;
        }
        static async Task<LiteCollection<s_message>> dbmain(long owner)
        {
            var dv = await getdb(owner);
            return dv.GetCollection<s_message>("main");
        }
        async static Task<LiteCollection<diff>> dbdiff(long owner)
        {
            var dv = await getdb(owner);
            return dv.GetCollection<diff>("diff");
        }
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
        public static async Task upsert(long sender, long reciver, s_message message)
        {
            await upsert_(sender, reciver, message, e_messagestate.send);
            await upsert_(reciver, sender, message, e_messagestate.receive);
        }
        static async Task upsert_(long owner, long partner, s_message message, e_messagestate messagestate)
        {
            message.partner = partner;
            message.messagestate = messagestate;
            (await dbmain(owner)).Upsert(message);
            diff.set(await dbdiff(owner), message.id, difftype.update);
            mainserver.notify(owner);
        }
        public static async Task<q_loaddiff.done> loaddiff(long owner, long index)
        {
            return diff.loaddiff(await dbmain(owner), await dbdiff(owner), index);
        }
    }
}