using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.common;
using Dna.user;

namespace user_service
{
    class logout : myservice<q_logout>
    {
        public async override Task<answer> getanswer(q_logout question)
        {
            await Task.CompletedTask;
            dbtoken.Delete(i => i.token == question.token);
            return new voidanswer();
        }
    }
}