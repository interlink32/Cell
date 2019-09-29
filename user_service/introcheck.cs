using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class introcheck : my_server<q_introcheck>
    {
        public async override Task<answer> get_answer(q_introcheck request)
        {
            await Task.CompletedTask;
            var dv = db_intro.FindOne(i => i.value == request.introcode);
            if (dv == null)
                return new q_introcheck.invalidcode();
            else
                return new q_introcheck.done() { userid = dv.user_id };
        }
    }
}