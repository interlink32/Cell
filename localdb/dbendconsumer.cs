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
    public class dbendconsumer<entity, contact> : dbend<entity, contact> where entity : s_entity where contact : s_contact
    {
        public dbendconsumer(long receiver) : base(receiver)
        {
            runing();
        }
        public ObservableCollection<full> list => new ObservableCollection<full>();
        Expression<Func<full, bool>> func;
        public void search(Expression<Func<full, bool>> func)
        {
            this.func = func;
            var dv = dbfull.Find(func);
            list.Clear();
            foreach (var i in dv)
                list.Add(i);
        }

        long localindex = 0;
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
        public event Action<full> update_e;
        void sync()
        {
            var dv = dbdiff.Find(i => i.index > localindex).ToArray();
            var updated = dv.Where(i => i.state ==  difftype.update).Select(i => i.itemid).ToArray();
            var deleted = dv.Where(i => i.state ==  difftype.delete).Select(i => i.itemid).ToArray();
            foreach (var i in deleted)
            {
                var item = list.FirstOrDefault(j => j.id == i);
                list.Remove(item);
            }
            foreach (var i in updated)
            {
                var olditem = list.FirstOrDefault(j => j.id == i);
                var newitem = dbfull.FindOne(j => j.id == i);
                if (olditem == null)
                {
                    if (func != null && func.Compile().Invoke(newitem))
                        list.Add(newitem);
                }
                else
                {
                    olditem.contact = newitem.contact;
                    olditem.entity = newitem.entity;
                }
                update_e?.Invoke(newitem);
            }
        }
        public bool exists(Expression<Func<full, bool>> func)
        {
            return dbfull.Exists(func);
        }
    }
}