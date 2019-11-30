using Dna;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2searchuser : myservice<q_searchuser>
    {
        public async override Task<answer> getanswer(q_searchuser question)
        {
            await Task.CompletedTask;
            var dv = db.find(i => i.general && i.active).Select(i => i.clone());
            if (question.nationalcode != null)
                dv = dv.Where(i => i.nationalcode.Contains(question.nationalcode));
            if (question.fullname != null)
                dv = dv.Where(i => i.fullname.Contains(question.fullname));
            if (question.city != null)
                dv = dv.Where(i => i.city.Contains(question.city));
            if (question.description != null)
                dv = dv.Where(i => i.description.Contains(question.description));
            if (question.nature != null)
                dv = dv.Where(i => i.nature == question.nature);
            if (question.tell != null)
                dv = dv.Where(i => i.tell.Contains(question.tell));
            return new q_searchuser.done() { users = dv.ToArray() };
        }
    }
}