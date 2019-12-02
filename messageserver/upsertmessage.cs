using Connection;
using Dna;
using Dna.message;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    class upsertmessage : service<q_upsertmessage>
    {
        public async override Task<answer> getanswer(q_upsertmessage question)
        {
            //db.send(new s_message()
            //{
            //    sender = question.z_user,
            //    receiver = question.partner,
            //    text = question.text,
            //    time = DateTime.Now
            //});
            await Task.CompletedTask;
            return null;
        }
    }
}