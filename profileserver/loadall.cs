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
            List<long> ids = new List<long>(question.ids);
            List<r_profile> l = new List<r_profile>();
            foreach (var i in question.ids)
            {
                var ddd = s.dbprofile.FindOne(j => ids.Contains(i));
                if (ddd != null)
                    l.Add(ddd);
                ddd = null;
                ids.Remove(i);
            }
            return new q_loadallprofile.done()
            {
                profiles = l.Select(i => i.clone()).ToArray()
            };
        }
    }
}