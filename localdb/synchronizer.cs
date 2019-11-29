using Connection;
using Dna;
using Dna.common;
using Dna.userdata;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace localdb
{
    public abstract class synchronizer<entity> where entity : s_entity
    {
        readonly string server;
        public readonly long userid;
        public client client = null;
        public synchronizer(long userid)
        {
            this.server = gene.get(typeof(entity)).chromosom;
            this.userid = userid;
            client = new client(userid);
            client.notifyadd(gene.get(server), userid, sync);
        }
        public void close()
        {
            client.notifyremove(sync);
        }
        protected virtual async void sync(long obj)
        {
            long index = dbindex.get(indexid);
            var rsv = await client.question(userid, new q_loaddiff(server) { index = index }) as q_loaddiff.done;
            if (rsv.entites != null)
            {
                var dv = JsonConvert.DeserializeObject<entity[]>(rsv.entites);
                foreach (var i in dv)
                    apply(i);
            }
            foreach (var i in rsv.deleted)
                delete(i);
            dbindex.set(indexid, rsv.currentindex);
        }
        protected abstract void apply(entity entity);
        protected abstract void delete(long entity);
        protected virtual string indexid => "sync" + typeof(entity).FullName + userid;
    }
}