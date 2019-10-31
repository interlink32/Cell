using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class loadentity : myservice<q_loaddiff>
    {
        public async override Task<answer> getanswer(q_loaddiff question)
        {
            await Task.CompletedTask;
            var dv = dbdiff.Find(i => i.index > question.index).Take(100).ToArray();
            var updated = dv.Where(i => i.state == r_diffstate.update).Select(i => i.userid).ToArray();
            var deleted = dv.Where(i => i.state == r_diffstate.delete).Select(i => i.userid).ToArray();
            return new q_loaddiff.doen()
            {
                updatedentity = updated,
                deletedentity = deleted,
                currentindex = dv.LastOrDefault()?.index ?? question.index
            };
        }
    }
}