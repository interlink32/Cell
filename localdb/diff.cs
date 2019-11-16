using Dna.common;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace localdb
{
    public class diff
    {
        [BsonId]
        public long index { get; set; }
        public long itemid { get; set; }
        public difftype state { get; set; }
        public static void set(LiteCollection<diff> collection, long partnerid, difftype state)
        {
            if (state == difftype.delete)
                collection.Delete(i => i.itemid == partnerid);
            else
                collection.Delete(i => i.itemid == partnerid && i.state == state);
            collection.Insert(new diff()
            {
                itemid = partnerid,
                state = state
            });
        }
        public static q_loaddiff.done getdiff(LiteCollection<diff> collection, long index)
        {
            var dv = collection.Find(i => i.index > index);
            return new q_loaddiff.done()
            {
                currentindex = dv.LastOrDefault()?.index ?? index,
                deletedentity = dv.Where(i => i.state == difftype.delete).Select(i => i.itemid).ToArray(),
                updatedentity = dv.Where(i => i.state == difftype.update).Select(i => i.itemid).ToArray()
            };
        }
        public static q_loaddiffcontact.done getdiffcontact(LiteCollection<diff> collection, long index)
        {
            var dv = collection.Find(i => i.index > index);
            return new q_loaddiffcontact.done()
            {
                currentindex = dv.LastOrDefault()?.index ?? index,
                deleted = dv.Where(i => i.state == difftype.delete).Select(i => i.itemid).ToArray(),
                updatedentity = dv.Where(i => i.state == difftype.update).Select(i => i.itemid).ToArray(),
                updatedcontact = dv.Where(i => i.state == difftype.updatecontact).Select(i => i.itemid).ToArray()
            };
        }
    }
    public enum difftype
    {
        delete,
        update,
        updatecontact,
    }
}