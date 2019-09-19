using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.message;

namespace message_server
{
    class send : my_service<q_send>
    {
        public async override Task<answer> get_answer(q_send question)
        {
            await Task.CompletedTask;
            message message = new message()
            {
                sender = question.z_user,
                text = question.text,
                time = DateTime.Now
            };
            db_message.Insert(message);
            notify(question.chat_id, new n_new_message());
            return new q_send.doen()
            {
                message = message.create()
            };
        }
    }
}