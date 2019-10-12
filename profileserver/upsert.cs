using Connection;
using Dna;
using Dna.profile;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class upsert : myservice<q_upsert>
    {
        public async override Task<answer> getanswer(q_upsert question)
        {
            await Task.CompletedTask;
            var profile = dbprofile.FindOne(i => i.id == question.z_user);
            if (profile == null)
            {
                profile = new r_profile()
                {
                    id = question.z_user
                };
            }
            profile.address = question.address;
            if (profile.fullname != question.fullname)
            {
                profile.fullname = question.fullname;
                await server.q(new q_rename()
                {
                    user = question.z_user,
                    fullname = question.fullname
                });
            }
            profile.gender = question.gender;
            profile.nationalcode = question.nationalcode;
            profile.tell = question.tell;
            dbprofile.Upsert(profile);
            return null;
        }
    }
}