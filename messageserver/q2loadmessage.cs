using Connection;
using Dna;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    class q2loadmessage : service<q_loaddiff>
    {
        public async override Task<answer> getanswer(q_loaddiff question)
        {
            await Task.CompletedTask;
            return db.loaddiff(question.z_user, question.index);
        }
    }
}