using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class clientitem
    {
        public readonly long userid;
        public readonly string chromosome;
        internal const int pulsenotify = -1;
        public bool isnotifier = true;
        public clientitem(long userid, string chromosome)
        {
            this.userid = userid;
            this.chromosome = chromosome;
        }
        protected abstract Task cycle();
        public void close()
        {

        }
        internal async Task<answer> q(question question)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}