using Connection;
using Dna;
using Dna.common;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public abstract class syncentity<entity1, entity2> where entity1 : s_entity where entity2 : s_entity
    {
        private readonly e_chromosome chromosome1;
        public syncentity(e_chromosome chromosome1, e_chromosome chromosome2)
        {
            client.notifyadd(chromosome1, chromosome2, action);
            this.chromosome1 = chromosome1;
        }
        async void action(long obj)
        {
            var dv = await client.question(new q_loaddiff(chromosome1) { index = index }) as q_loaddiff.doen;
            
        }
        public abstract LiteCollection<entity2> tabelentity { get; }
        protected abstract LiteCollection<diffentity> tablediff { get; }
        protected abstract LiteCollection<s_index> tableindex { get; }
        protected abstract entity2 update(entity1 entity);
        string z_indexname = typeof(entity1).FullName + typeof(entity2).FullName;
        long index
        {
            get
            {
                return tableindex.FindOne(i => i.id == z_indexname)?.index ?? 0;
            }
            set
            {
                tableindex.Upsert(new s_index()
                {
                    id = z_indexname,
                    index = value
                });
            }
        }
    }
}