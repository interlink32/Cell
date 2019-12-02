using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace servercell
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
        public abstract Task<answer> getanswer(T question);
        internal sealed async override Task<answer> z_get_answer(question question)
        {
            var dv = await getanswer(question as T);
            return dv;
        }
    }
}