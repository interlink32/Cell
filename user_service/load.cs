using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class load : myservice<q_load>
    {
        public async override Task<answer> getanswer(q_load question)
        {
            await Task.CompletedTask;
            var dv = dbuser.FindOne(i => i.id == question.userid);
            return new q_load.done()
            {
                user = new s_user()
                {
                    id = dv.id,
                    fullname = dv.fullname
                }
            };
        }
    }
}