using Dna;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class searchprofile : myservice<q_searchprofile>
    {
        public async override Task<answer> getanswer(q_searchprofile question)
        {
            nullcheck(ref question.fullname);
            nullcheck(ref question.nationalcode);
            nullcheck(ref question.slogan);

            await Task.CompletedTask;
            var qu = s.dbprofile.FindAll();
            if (question.fullname != null)
                qu = qu.Where(i => i.fullname.Contains(question.fullname));
            if (question.nationalcode != null)
                qu = qu.Where(i => i.nationalcode == question.nationalcode);
            if (question.nature != e_nature.none)
                qu = qu.Where(i => i.nature == question.nature);
            if (question.slogan != null)
                qu = qu.Where(i => i.slogan.Contains(question.slogan));
            var dv = qu.Take(10).ToArray();
            return new q_searchprofile.done()
            {
                profiles = dv.Select(i => i.clone()).ToArray()
            };
        }
    }
}