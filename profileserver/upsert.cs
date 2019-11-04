using Connection;
using Dna;
using Dna.common;
using Dna.profile;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class upsert : myservice<q_updateprofile>
    {
        public async override Task<answer> getanswer(q_updateprofile question)
        {
            await Task.CompletedTask;
            var profile = sync.dbentity.load(question.z_user);
            if (profile == null)
                return new developererror() { code = "lblkbjdjvjchdhvjdnvjcjds" };
            profile.city = question.city;
            profile.nature = question.gender;
            profile.nationalcode = question.nationalcode;
            profile.tell = question.tell;
            sync.dbentity.upsert(profile);
            notify(e_chromosome.usercontact);
            notify(question.z_user);
            return null;
        }
    }
}