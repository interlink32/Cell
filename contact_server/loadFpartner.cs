using Dna;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class loadFpartner : my_service<q_loadFpartner>
    {
        public async override Task<answer> get_answer(q_loadFpartner question)
        {
            await Task.CompletedTask;
            var sss = db_contact.FindAll().ToArray();
            var dv = db_contact.FindOne(i => check(question, i));
            if (dv == null)
            {
                dv = new contact()
                {
                    members = new member[]
                    {
                        new member()
                        {
                            person=question.z_user
                        },
                        new member()
                        {
                            person=question.partner
                        }
                    }
                };
                db_contact.Insert(dv);
            }
            return new q_loadFpartner.done()
            {
                contact = dv.clone()
            };
        }
        private static bool check(q_loadFpartner question, contact i)
        {
            return i.any(question.z_user, question.partner);
        }
    }
}