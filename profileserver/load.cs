using Dna;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class load : myservice<q_load>
    {
        public async override Task<answer> getanswer(q_load question)
        {
            await Task.CompletedTask;
            var profile = s.dbprofile.FindOne(i => i.id == question.userid);
            if (profile == null)
                profile = new r_profile();
            return new q_load.done()
            {
                profile = profile.clone()
            };
        }
    }
}