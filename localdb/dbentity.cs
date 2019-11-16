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
        LiteCollection<diff> tablediff => db.GetCollection<diff>();

        public bool exists(Expression<Func<entity, bool>> func)
        {
            return table.Exists(func);
        }

        public void upsert(entity entity, bool savediff = true)
        {
            table.Upsert(entity);
            if (savediff)
                diff.set(tablediff, entity.id, difftype.update);
        }
        public void delete(long id)
        {
            table.Delete(i => i.id == id);
            diff.set(tablediff, id, difftype.delete);
        }
        public q_loaddiff.done getdiff(long index)
        {
            return diff.getdiff(tablediff, index);
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