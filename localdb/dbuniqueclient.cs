using controllibrary;
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
    public class dbuniqueclient<entity, contact> : dbunique<entity, contact> where entity : s_entity where contact : s_contact
    {
        public readonly ObservableCollection<entitycontact<entity, contact>> list = new ObservableCollection<entitycontact<entity, contact>>();
        public dbuniqueclient(long userid, e_chromosome chromosome) : base(userid, chromosome)
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
                var rsv = dbentitydiff.Find(i => i.index > localindex).ToArray();
                foreach (var i in rsv)
                {
                    var dv = list.FirstOrDefault(j => j.partnerid == i.itemid);
                    if (dv != null)
                        dv.partner = dbentity.FindOne(j => j.id == i.itemid);
                }
            }
            {
                var rsv = dbcontactdiff.Find(i => i.index > localindex).ToArray();
                foreach (var i in rsv)
                {
                    var dv = list.FirstOrDefault(j => j.partnerid == i.itemid);
                    if (dv != null)
                        dv.Contact = dbcontact.FindOne(j => j.partnerid == i.itemid);
                }
            }

            uibase.run(refresh_e);
        }

        Expression<Func<entity, bool>> entitysearch = default;
        Expression<Func<contact, bool>> contactsearch = default;
        public void search(Expression<Func<entity, bool>> entitysearch, Expression<Func<contact, bool>> contactsearch = default)
        {
            this.entitysearch = entitysearch;
            this.contactsearch = contactsearch;
            list.Clear();
            if (this.entitysearch == null && this.contactsearch == null)
                return;
            if (entitysearch == null)
            {
                var dv = dbcontact.Find(contactsearch).ToArray();
                foreach (var i in dv)
                {
                    list.Add(new entitycontact<entity, contact>()
                    {
                        Contact = i,
                        partner = dbentity.FindOne(j => j.id == i.partnerid)
                    });
                }
            }
            else
            if (contactsearch == null)
            {
                var dv = dbentity.Find(entitysearch).ToArray();
                foreach (var i in dv)
                {
                    list.Add(new entitycontact<entity, contact>()
                    {
                        partner = i,
                        Contact = dbcontact.FindOne(j => j.partnerid == i.id)
                    });
                }
            }
            else
            {
                var entity = dbentity.Find(entitysearch);
                var contacts = dbcontact.Find(contactsearch);
                var dv = from i in entity
                         join j in contacts on i.id equals j.partnerid
                         select new entitycontact<entity, contact>()
                         {
                             Contact = j,
                             partner = i,
                             partnerid = i.id
                         };
                foreach (var i in dv.ToArray())
                    list.Add(i);
            }
        }
        public bool connectto(long id)
        {
            var rsv = dbcontact.FindAll().ToArray();
            return dbcontact.Exists(i => i.partnerid == id);
        }
    }
}