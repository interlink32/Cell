using Dna;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class create : my_service<q_create>
    {
        public async override Task<answer> get_answer(q_create question)
        {
            await Task.CompletedTask;
            var dv = db_contact.FindOne(i => i.members.Any(j => j.person == question.person && j.person == question.z_user));
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
                            person=question.person
                        }
                    }
                };
                db_contact.Insert(dv);
            }
            return new q_create.done()
            {
                contact = dv.clone()
            };
        }
    }
}