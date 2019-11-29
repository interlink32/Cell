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
            var dv = db.find(i => i.general && i.active).Select(i => i.clone()).ToArray();
            return new q_searchuser.done() { users = dv };
        }
    }
}