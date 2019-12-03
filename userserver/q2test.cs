using Connection;
using Dna;
using Dna.user;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2test : service<q_test>
    {
        public async override Task<answer> getanswer(q_test question)
        {
            await Task.CompletedTask;
            return new q_test.done()
            {
                output = question.input
            };
        }
    }
}