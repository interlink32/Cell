using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class service_gene
    {
        internal service_gene() { }
        internal abstract Task<response> z_get_answer(request request);
        internal abstract string z_gene { get; }
    }
    public abstract class service_gene<T> : service_gene where T : request
    {
        internal sealed override string z_gene { get; }
        public service_gene()
        {
            z_gene = typeof(T).Name;
        }
        public abstract Task<response> get_answer(T request);
        internal sealed override Task<response> z_get_answer(request request)
        {
            return get_answer(request as T);
        }
    }
}