using Dna;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class load : myservice<q_loadprofile>
    {
        public async override Task<answer> getanswer(q_loadprofile question)
        {
            await Task.CompletedTask;
            var dv = s.dbprofile.FindOne(i => i.id == question.id);
            return new q_loadprofile.done()
            {
                profile = dv.clone()
            };
        }
    }
}