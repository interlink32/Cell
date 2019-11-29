using Dna;
using Dna.common;
using Dna.userdata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2loaddiff : myservice<q_loaddiff>
    {
        public async override Task<answer> getanswer(q_loaddiff question)
        {
            await Task.CompletedTask;
            return db.loaddiff(question.z_user, question.index);
        }
    }
}