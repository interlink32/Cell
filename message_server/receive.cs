using Dna;
using Dna.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace message_server
{
    class receive : my_service<q_receive>
    {
        public async override Task<answer> get_answer(q_receive question)
        {
            await Task.CompletedTask;
            var dv = db_message.Find(i => i.chat_id >= question.first_index && i.chat_id <= question.last_index).Select(i => i.create()).ToArray();
            return new q_receive.done()
            {
                messages = dv
            };
        }
    }
}