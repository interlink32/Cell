using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.common;
using Dna.contact;
using Dna.message;

namespace message_server
{
    class send : my_service<q_send>
    {
        public async override Task<answer> get_answer(q_send question)
        {
            var rsv = await q(new q_loadFcontact()
            {
                contact = question.contact
            }) as q_loadFcontact.done;
            if (rsv.contact == null)
                return new developer_error() { code = "fjdhgdmfdndsssfmdfhdhdvf" };
            if (!rsv.contact.included(question.z_user))
                return new developer_error() { code = "khjfjffdjbndjbjfjbjfnbj" };
            else
            {
                message mes = new message()
                {
                    sender = question.z_user,
                    text = question.text,
                    time = DateTime.Now
                };
                db_message(rsv.contact.id).Insert(mes);
                foreach (var i in rsv.contact.members)
                    notify(i.person, new n_message() { contact = rsv.contact.id });
                return null;
            }
        }
    }
}