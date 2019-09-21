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
            var co = db_message(question.contact);
            var all = co.FindAll().ToArray();
            var dv = co.Find(i => i.id >= question.first_index && i.id <= question.last_index).ToArray();
            return new q_receive.done()
            {
                messages = dv.Select(i => i.create()).ToArray()
            };
        }
    }
}