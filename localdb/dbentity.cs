using Connection;
using Dna;
using Dna.common;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace localdb
{
    public abstract class dbentity<entity> where entity : s_entity
    {
        public abstract LiteCollection<entity> table { get; }
        protected abstract LiteCollection<diffentity> tablediff { get; }
        public void upsert(entity entity)
        {
            table.Upsert(entity);
            setdiff(entity.id, true);
        }
        public void delete(long id)
        {
            table.Delete(i => i.id == id);
            setdiff(id, false);
        }
        public q_loaddiff.doen getdiff(long index)
        {
            var dv = tablediff.Find(i => i.index > index).ToArray();
            return new q_loaddiff.doen()
            {
                currentindex = dv.LastOrDefault()?.index ?? index,
                updatedentity = dv.Where(i => i.state).Select(i => i.itemid).ToArray(),
                deletedentity = dv.Where(i => !i.state).Select(i => i.itemid).ToArray()
            };
        }
        private void setdiff(long id, bool state)
        {
            tablediff.Delete(i => i.itemid == id);
            tablediff.Insert(new diffentity()
            {
                itemid = id,
                state = state
            });
        }
    }
}