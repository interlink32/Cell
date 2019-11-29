using Dna;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace localdb
{
    public class syncdb<input, output> : dbend<input> where input : s_entity where output : Is_entity, new()
    {
        public ocollection<output> list = new ocollection<output>();
        public syncdb(long userid) : base(userid)
        {

        }
        public override event Action<long> delete_e;
        internal override void delete(long entity)
        {
            var dv = list.FirstOrDefault(i => i.id == entity);
            if (dv != null)
                list.Remove(dv);
            delete_e?.Invoke(entity);
        }
        public new event Action<output> update_e;
        public event Action<output> add_e;
        internal override void update(input entity)
        {
            var dv = list.FirstOrDefault(i => i.id == entity.id);
            bool newitem = false;
            if (dv == null)
            {
                dv = new output();
                newitem = true;
            }
            dv.update(userid, entity);
            if (newitem)
            {
                list.Add(dv);
                add_e?.Invoke(dv);
            }
            else
                list.reset();
            update_e?.Invoke(dv);
        }
    }
    public class syncdb<entity> : syncdb<entity, entity> where entity : s_entity, new()
    {
        public syncdb(long userid) : base(userid)
        {
        }
    }
}