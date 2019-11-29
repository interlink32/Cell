using Dna;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class synccenter<entity> : synchronizer<entity> where entity : s_entity
    {
        localdb<entity> dbend;
        public synccenter(long userid) : base(userid)
        {
            dbend = new localdb<entity>(userid);
            dbend.dbdiff.Delete(i => true);
        }
        protected override void apply(entity entity)
        {
            var dv = dbend.dbmain.FindOne(i => i.id == entity.id);
            if (dv == null)
                dv = entity;
            dv.update(userid, entity);
            dbend.dbmain.Upsert(dv);
            diff.set(dbend.dbdiff, entity.id, difftype.update);
        }
        protected override void delete(long entity)
        {
            dbend.dbmain.Delete(i => i.id == entity);
            diff.set(dbend.dbdiff, entity, difftype.delete);
        }
    }
}