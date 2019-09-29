using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class create_user : my_server<q_create_user>
    {
        public async override Task<answer> get_answer(q_create_user question)
        {
            await Task.CompletedTask;
            var dv = db_user.FindOne(i => i.user_name == question.user_id);
            if (dv == null)
            {
                dv = new s_user()
                {
                    user_name = question.user_id,
                    password = question.password
                };
                db_user.Insert(dv);
                return new q_create_user.done();
            }
            else
            {
                return new q_create_user.duplicate();
            }
        }
    }
}