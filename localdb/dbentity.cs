using Connection;
using Dna;
using Dna.common;
using Dna.user;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace localdb
{
    public class dbentity<entity> where entity : s_entity
    {
        LiteDatabase db;
        public dbentity(string fileid)
        {
            db = new LiteDatabase(reference.root(fileid));
        }
        LiteCollection<entity> table => db.GetCollection<entity>();
        public LiteCollection<T> GetCollection<T>(string name)
        {
            return db.GetCollection<T>(name);
        }
        LiteCollection<diffentity> tablediff => db.GetCollection<diffentity>();

        public bool exists(Expression<Func<entity, bool>> func)
        {
            return table.Exists(func);
        }

        public void upsert(entity entity, bool savediff = true)
        {
            table.Upsert(entity);
            if (savediff)
                setdiff(entity.id, true);
        }
        public void delete(long id)
        {
            table.Delete(i => i.id == id);
            setdiff(id, false);
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
        public entity load(long id)
        {
            return table.FindOne(i => i.id == id);
        }
        public entity findone(Expression<Func<entity,bool>> func)
        {
            return table.FindOne(func);
        }
        public entity[] load(params long[] ids)
        {
            List<long> idlist = new List<long>(ids);
            List<entity> l = new List<entity>();
            foreach (var i in ids)
            {
                var dv = table.FindOne(j => idlist.Contains(j.id));
                if (dv != null)
                    l.Add(dv);
                idlist.Remove(i);
            }
            return l.ToArray();
        }
        public IEnumerable<entity> getall()
        {
            return table.FindAll();
        }
    }
}