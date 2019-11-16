using Connection;
using Dna;
using Dna.common;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace localdb
{
    public abstract class synchronizer<entity> where entity : s_entity
    {
        public readonly e_chromosome server;
        public readonly long user;
        public client client = null;
        public synchronizer(e_chromosome server, long user)
        {
            this.server = server;
            this.user = user;
            client = new client(user);
            client.notifyadd(server, user, sync);
        }
        public void close()
        {
            client.notifyremove(sync);
        }
        protected virtual async void sync(long obj)
        {
            long index = dbindex.get(indexid);
            var rsv = await client.question(new q_loaddiff(server) { index = index }, user)
                as q_loaddiff.done;
            if (rsv.updatedentity.Length != 0)
            {
                var entites = await getentities(rsv.updatedentity);
                foreach (var i in entites)
                    apply(i);
            }
            foreach (var i in rsv.deletedentity)
                delete(i);
            dbindex.set(indexid, rsv.currentindex);
        }
        protected abstract Task<entity[]> getentities(long[] ids);
        protected abstract void apply(entity entity);
        protected abstract void delete(long entity);
        protected virtual string indexid => "sync" + GetType().FullName + typeof(entity).FullName;
    }
    public abstract class synchronizer<entity, contact> : synchronizer<entity> where entity : s_entity where contact : s_contact
    {
        public synchronizer(e_chromosome server, long user) : base(server, user)
        {
        }
        protected async override void sync(long user)
        {
            var rsv1 = await client.question(new q_loaddiffcontact(server) { index = dbindex.get(indexid) }, user);
            var rsv = rsv1 as q_loaddiffcontact.done;
            if (rsv.updatedentity.Length != 0)
            {
                var dv = await getentities(rsv.updatedentity);
                foreach (var i in dv)
                    apply(i);
            }
            if (rsv.updatedcontact.Length != 0)
            {
                var dv = await getcontacts(rsv.updatedcontact);
                foreach (var i in dv)
                    apply(i);
            }
            dbindex.set(indexid, rsv.currentindex);
        }
        protected abstract void apply(contact contact);
        protected abstract Task<contact[]> getcontacts(long[] ids);
    }
}