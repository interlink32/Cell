using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class loadalluser : myservice<q_loadalluser>
    {
        public async override Task<answer> getanswer(q_loadalluser question)
        {
            await Task.CompletedTask;
            nullcheck(ref question.name_filter);
            var dv = dbuser.FindAll();
            if (question.ids_filter != null)
                dv = dv.Where(i => question.ids_filter.Contains(i.id));
            if (question.name_filter != null)
                dv = dv.Where(i => i.callerid.Contains(question.name_filter));
            return new q_loadalluser.done()
            {
                users = dv.Select(i => i.clone()).Skip(question.skep).Take(20).ToArray()
            };
        }
    }
}