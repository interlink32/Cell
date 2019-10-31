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
            var dv = s.dbdiff.Find(i => i.index > question.index).ToArray();
            return new q_loaddiff.doen()
            {
                currentindex = dv.LastOrDefault()?.index ?? 0,
                updatedentity = dv.Where(i => i.state == 1).Select(i => i.itemid).ToArray(),
                deletedentity = dv.Where(i => i.state == 0).Select(i => i.itemid).ToArray()
            };
        }
    }
}