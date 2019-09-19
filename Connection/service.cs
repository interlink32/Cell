using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class service
    {
        internal server server;
        internal service() { }
        internal abstract Task<answer> z_get_answer(question question);
        internal abstract string z_gene { get; }
        public async Task<answer> q(question question)
        {
            return await server.question(question);
        }
    }
    public abstract class service<T> : service where T : question
    {
        internal sealed override string z_gene { get; }
        public service()
        {
            z_gene = typeof(T).Name;
        }
        public void notify(long receiver, notify notify)
        {
            server.send_notify(receiver, notify);
        }
        public abstract Task<answer> get_answer(T question);
        internal sealed async override Task<answer> z_get_answer(question question)
        {
            var dv = await get_answer(question as T);
            return dv;
        }
    }
}