using Connection;
using Dna;
using Dna.message;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace message_server
{
    class getsituation : my_service<q_getsituation>
    {
        public async override Task<answer> get_answer(q_getsituation question)
        {
            await Task.CompletedTask;
            return new q_getsituation.done()
            {
                last_visit = db_situation.FindOne(i => i.user == question.partner)?.last_visit
            };
        }
    }
}