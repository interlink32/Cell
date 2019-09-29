using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace user_service
{
    class login : my_server<q_login>
    {
        static Random random = new Random();
        public async override Task<answer> get_answer(q_login question)
        {
            await Task.CompletedTask;
            var user = db_user.FindOne(i => i.user_name == question.user_name);
            if (user != null && user.password == question.password)
            {
                var token = db_device.FindOne(i => i.user == user.id);
                if (token == null)
                {
                    token = new s_device()
                    {
                        user = user.id,
                        id = random.NextDouble()
                    };
                    db_device.Upsert(token);
                }
                return new q_login.done()
                {
                    id = user.id,
                    device = token.id
                };
            }
            else
                return new q_login.invalid();
        }
    }
}