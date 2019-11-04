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
            return new q_loadprofile.done()
            {
                profile = sync.dbentity.load(question.id)?.clone()
            };
        }
    }
}