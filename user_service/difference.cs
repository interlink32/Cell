using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class difference : myservice<q_differenceusers>
    {
        public async override Task<answer> getanswer(q_differenceusers question)
        {
            await Task.CompletedTask;
            var dv = dbdiff.Find(i => i.index > question.index).Take(100).ToArray();
            var updated = dv.Where(i => i.state == r_diffstate.update).Select(i => i.userid).ToArray();
            var deleted = dv.Where(i => i.state == r_diffstate.delete).Select(i => i.userid).ToArray();
            return new q_differenceusers.doen()
            {
                updated = dbuser.Find(i => updated.Contains(i.id)).Select(i => i.clone()).ToArray(),
                deleted = deleted,
                currentindex = dv.LastOrDefault()?.index ?? question.index
            };
        }
    }
}