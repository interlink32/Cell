using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        public readonly long id;
        public client(long id)
        { 
            this.id = id;
            string path = reference.root("");
            Directory.CreateDirectory(path);
            send_pulse();
            create();
        }
        async void create()
        {
            await qlocking.WaitAsync();
            foreach (var i in await basic.allchromosome())
                qlist.Add(new questioner(id, i));
            qlocking.Release();
        }
        public event Action<notify> notify_e;
        internal void notify(notify notify)
        {
            notify_e?.Invoke(notify);
        }
        SemaphoreSlim qlocking = new SemaphoreSlim(1, 1);
        public async Task<answer> question(question question)
        {
            await qlocking.WaitAsync();
            var dv = qlist.FirstOrDefault(i => i.info.chromosome.ToString() == question.z_chromosome);
            if (dv == null)
            {
                dv = new questioner(id, await basic.getchromosome(question.z_chromosome));
                qlist.Add(dv);
            }
            qlocking.Release();
            return await dv.question(question);
        }
        List<notifier> nlist = new List<notifier>();
        SemaphoreSlim nlocking = new SemaphoreSlim(1, 1);
        public async void active_notify(e_chromosome chromosome)
        {
            await nlocking.WaitAsync();
            var dv = nlist.FirstOrDefault(i => i.info.chromosome == chromosome);
            if (dv == null)
                nlist.Add(new notifier(id, await basic.getchromosome(chromosome.ToString())));
            nlocking.Release();
        }
        async void send_pulse()
        {
            await nlocking.WaitAsync();
            foreach (var i in nlist)
                i.send();
            nlocking.Release();
            await Task.Delay(1000);
            send_pulse();
        }

        List<questioner> qlist = new List<questioner>();
        public long z_user { get; private set; }
        public void close()
        {
            foreach (var i in nlist)
                i.close();
            foreach (var i in qlist)
                i.close();
        }
    }
}