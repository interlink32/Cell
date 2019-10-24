using Connection;
using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class sync
    {
        long index = default;
        public sync()
        {
            var dv = s.dbsingle.FindOne(i => i.id == nameof(index));
            if (dv == null)
            {
                dv = new singlevalue()
                {
                    id = nameof(index),
                    value = "0"
                };
                s.dbsingle.Insert(dv);
            }
            index = long.Parse(dv.value);
            client.notifyadd(e_chromosome.user, e_chromosome.profile, reset);
        }
        async void reset(long obj)
        {
            var dv = await mainserver.q(new q_loadentity(e_chromosome.user) { index = index }) as q_loadentity.doen;
            await update(dv.updatedentity);
            delete(dv.deletedentity);
            index = dv.currentindex;
            saveindex();
        }
        private void saveindex()
        {
            s.dbsingle.Upsert(new singlevalue()
            {
                id = nameof(index),
                value = index.ToString()
            });
        }
        private void delete(long[] ids)
        {
            if (ids.Length == 0)
                return;
            foreach (var i in ids)
            {
                s.dbprofile.Delete(j => j.id == i);
                s.dbdiff.Delete(j => j.itemid == i);
                s.dbdiff.Insert(new r_diff()
                {
                    itemid = i,
                    state = 0
                });
                mainserver.sendnotify(e_chromosome.contact);
            }
        }
        async Task update(long[] ids)
        {
            if (ids.Length == 0)
                return;
            var dv = await mainserver.q(new q_loadalluser() { ids = ids }) as q_loadalluser.done;
            r_profile profile;
            foreach (var i in dv.users)
            {
                profile = s.dbprofile.FindOne(j => j.id == i.id);
                if (profile == null)
                    profile = new r_profile() { id = i.id };
                profile.fullname = i.fullname;
                s.dbprofile.Upsert(profile);
                s.dbdiff.Delete(j => j.itemid == i.id);
                s.dbdiff.Insert(new r_diff()
                {
                    itemid = i.id,
                    state = 1
                });
                mainserver.sendnotify((long)e_chromosome.contact);
                mainserver.sendnotify(i.id);
            }
        }
    }
}