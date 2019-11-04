using Dna;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class loadall : myservice<q_loadallprofile>
    {
        public async override Task<answer> getanswer(q_loadallprofile question)
        {
            await Task.CompletedTask;
            var dv = sync.dbentity.load(question.ids);
            return new q_loadallprofile.done()
            {
                profiles = dv.Select(i => i.clone()).ToArray()
            };
        }
    }
}