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
        public readonly e_chromosome sender;
        public readonly long receiver;
        public synchronizer(e_chromosome sender, long receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
            client.notifyadd(sender, receiver, sync);
        }
        public void close()
        {
            client.notifyremove(sync);
        }
        protected virtual async void sync(long obj)
        {
            var rsv = await client.question(new q_loaddiff(sender) { index = dbindex.get(indexid) }) as q_loaddiff.doen;
            if (rsv.updatedentity.Length != 0)
            {
                var entites = await getentities(rsv.updatedentity);
                foreach (var i in entites)
                    apply(i);
            }
            foreach (var i in rsv.deletedentity)
            {
                delete(i);
            }
            dbindex.set(indexid, rsv.currentindex);
        }
        protected abstract Task<entity[]> getentities(long[] ids);
        protected abstract void apply(entity entity);
        protected abstract void delete(long entity);
        protected abstract string indexid { get; }
    }
    public abstract class synchronizer<entity, contact> : synchronizer<entity> where entity : s_entity where contact : s_contact
    {
        public synchronizer(e_chromosome sender, long receiver) : base(sender, receiver)
        {
        }
        protected async override void sync(long user)
        {
            var rsv = await client.question(new q_loaddiffcontact(sender) { index = dbindex.get(indexid) }, user) as q_loaddiffcontact.done;
            if (rsv.updatedentity.Length != 0)
            {
                var dv = await getentities(rsv.updatedentity);
                foreach (var i in dv)
                    apply(i);
            }
            if (rsv.updatedcontact.Length != 0)
            {
                var dv = await getcontacts(rsv.updatedentity);
                foreach (var i in dv)
                    apply(i);
            }
        }
        protected abstract void apply(contact contact);
        protected abstract Task<contact[]> getcontacts(long[] ids);
    }
}