using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Connection
{
    class notifier : clientitem
    {
        public notifier(long userid, string chromosome) : base(userid, chromosome.ToString()) { }
        abstract class item
        {
            public string gene = default;
            public abstract void action(notify notify);
        }
        class item2<T> : item where T : notify
        {
            public Action<T> actionT = default;
            public override void action(notify notify)
            {
                actionT(notify as T);
            }
        }
        List<item> l = new List<item>();
        public void add<T>(Action<T> action) where T : notify, new()
        {
            T d = new T();
            if (d.z_chromosome != chromosome)
                throw new Exception("kkbjbjcjfnbncbxbbncnbmk");
            item2<T> item = new item2<T>()
            {
                actionT = action,
                gene = d.z_gene
            };
            l.Add(item);
        }
        protected async override Task cycle()
        {
            var dv = await read() as notify;
            var ddd = l.FirstOrDefault(i => i.gene == dv.z_gene);
            ddd?.action(dv);
        }
        internal void send()
        {
            try
            {
                if (connected)
                    tcp.GetStream().WriteByte(39);
            }
            catch
            {
                close();
            }
        }
    }
}