using Connection;
using controllibrary;
using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace localdb
{
    public class dbendreader<entity, contact> : dbend<entity, contact> where entity : s_entity where contact : s_contact
    {
        public readonly ObservableCollection<fullentity> list = new ObservableCollection<fullentity>();
        public dbendreader(long userid) : base(userid)
        {
            runing();
        }
        long localindex = 0;
        async void runing()
        {
            var newindex = index;
            if (newindex != localindex)
            {
                reset();
                localindex = newindex;
            }
            await Task.Delay(200);
            runing();
        }
        public event Action refresh_e;
        private void reset()
        {
            {
                var entites = dbdiff.Find(i => i.index > localindex).Select(i => i.itemid).ToArray();
                foreach (var i in entites)
                {
                    var fullentity = dbentity.FindOne(j => j.id == i);
                    var dv = list.FirstOrDefault(j => j.id == i);
                    if (dv == null)
                    {
                        if (func != null && func.Compile()(dv))
                            list.Add(dv);
                    }
                    else
                    {
                        dv.entity = fullentity.entity;
                        dv.contact = fullentity.contact;
                    }
                }
            }
            uibase.run(refresh_e);
        }
        Expression<Func<fullentity, bool>> func;
        public void search(Expression<Func<fullentity, bool>> func)
        {
            this.func = func;
            list.Clear();
            if (func == null)
                return;
            var dv = dbentity.Find(func);
            foreach (var i in dv)
                list.Add(i);
        }
    }
}