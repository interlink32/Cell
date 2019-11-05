using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class loadalluser : myservice<q_loadalluser>
    {
        public async override Task<answer> getanswer(q_loadalluser question)
        {
            await Task.CompletedTask;
            if (question.ids?.Length == 0)
                return new q_loadalluser.done();
            var dv = db.load(question.ids);
            return new q_loadalluser.done()
            {
                users = dv.Select(i => i.clone()).ToArray()
            };
        }
    }
}