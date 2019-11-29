using Dna;
using Dna.common;
using LiteDB;
using Newtonsoft.Json;
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
        public long entity { get; set; }
        public difftype state { get; set; }
        public static void set(LiteCollection<diff> collection, long entity, difftype state)
        {
            if (state == difftype.delete)
                collection.Delete(i => i.entity == entity);
            else
                collection.Delete(i => i.entity == entity && i.state == state);
            collection.Insert(new diff()
            {
                entity = entity,
                state = state
            });
        }
        public static q_loaddiff.done loaddiff<T>(LiteCollection<T> dbmain, LiteCollection<diff> dbdiff, long index) where T : s_entity
        {
            var dv = dbdiff.Find(i => i.index > index).ToArray();
            var updated = dv.Where(i => i.state == difftype.update).Select(i => i.entity).ToArray();
            string entitis = null;
            if (updated.Length != 0)
                entitis = JsonConvert.SerializeObject(dbmain.Find(i => updated.Contains(i.id)).ToArray());
            var deleted = dv.Where(i => i.state == difftype.delete).Select(i => i.entity).ToArray();
            return new q_loaddiff.done()
            {
                currentindex = dv.LastOrDefault()?.index ?? index,
                entites = entitis,
                deleted = deleted
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