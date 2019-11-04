using Dna;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class loadentity : myservice<q_loaddiff>
    {
        public async override Task<answer> getanswer(q_loaddiff question)
        {
            await Task.CompletedTask;
            return sync.dbentity.getdiff(question.index);
        }
    }
}