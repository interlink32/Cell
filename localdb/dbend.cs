using Dna;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace localdb
{
    public class dbend<entity> : localdb<entity> where entity : s_entity
    {
        public virtual event Action<entity> update_e;
        internal virtual void update(entity entity)
        {
            update_e?.Invoke(entity);
        }
        public virtual event Action<long> delete_e;
        internal virtual void delete(long entity)
        {
            delete_e?.Invoke(entity);
        }

        Expression<Func<entity, bool>> func = null;
        public dbend(long receiver) : base(receiver)
        {
            runing();
        }
        long localindex = 0;
        public event Action research_e;
        public void search(Expression<Func<entity, bool>> func)
        {
            this.func = func;
            research_e?.Invoke();
            var dv = dbmain.Find(func).ToList();
            foreach (var i in dv)
                update(i);
        }
        (long id, Action<entity> action) notifyf = (0, null);
        public void notify(long id, Action<entity> action)
        {
            notifyf = (id, action);
            var dv = dbmain.FindOne(i => i.id == id);
            if (dv != null)
                action?.Invoke(dv);
        }
        async void runing()
        {
            var newindex = dbdiff.Max(i => i.index).AsInt64;
            if (newindex != localindex)
            {
                sync();
                localindex = newindex;
            }
            await Task.Delay(200);
            runing();
        }
        void sync()
        {
            var dv = dbdiff.Find(i => i.index > localindex).ToArray();
            var updated = dv.Where(i => i.state == difftype.update).Select(i => i.entity).ToArray();
            var deleted = dv.Where(i => i.state == difftype.delete).Select(i => i.entity).ToArray();
            foreach (var i in deleted)
                delete(i);
            foreach (var i in updated)
            {
                var newitem = dbmain.FindOne(j => j.id == i);
                if (i == notifyf.id)
                    notifyf.action(newitem);
                var dvfunc = func;
                if (dvfunc == null)
                    dvfunc = j => true;
                if (dvfunc.Compile().Invoke(newitem))
                    update(newitem);
                else
                    delete(newitem.id);
            }
        }
    }
}