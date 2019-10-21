using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class service
    {
        internal mainserver server;
        internal service() { }
        internal abstract Task<answer> z_get_answer(question question);
        internal abstract string z_gene { get; }
    }
    public abstract class service<T> : service where T : question
    {
        internal sealed override string z_gene { get; }
        public service()
        {
            z_gene = typeof(T).Name;
        }
        public static async Task notify(long receiver)
        {
            await mainserver.sendnotify(receiver);
        }
        public static async Task notify(e_chromosome chromosome)
        {
            await mainserver.sendnotify((long)chromosome);
        }
        public abstract Task<answer> getanswer(T question);
        internal sealed async override Task<answer> z_get_answer(question question)
        {
            var dv = await getanswer(question as T);
            return dv;
        }
        public static void nullcheck(ref string val)
        {
            if (val == "")
                val = null;
        }
    }
}