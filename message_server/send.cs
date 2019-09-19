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
            }) as q_loadFpartner.done;
            if (rsv.contact == null)
                return new developer_error() { code = "fjdhgdmfdndsssfmdfhdhdvf" };
            if (!rsv.contact.included(question.z_user))
                return new developer_error() { code = "khjfjffdjbndjbjfjbjfnbj" };
            else
            {
                message mes = new message()
                {
                    sender = rsv.contact.id,
                    text = question.text,
                    time = DateTime.Now
                };
                db_message(mes.id).Insert(mes);
                notify(rsv.contact.another(question.z_user), new n_new_message());
                return new q_send.doen()
                {
                    message = mes.create()
                };
            }
        }
    }
}