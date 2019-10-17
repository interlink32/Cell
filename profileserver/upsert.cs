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
            bool fullnamechange = false;
            if (profile.fullname != question.fullname)
            {
                if (!valid(question.fullname))
                    return new developererror() { code = "kjdjbdnbkdmsnbjvndmnbnd" };
                if (dbprofile.Exists(i => i.fullname == question.fullname && i.id != profile.id))
                    return new q_upsert.duplicatename();
                profile.fullname = question.fullname;
                fullnamechange = true;
            }
            profile.address = question.address;
            profile.gender = question.gender;
            profile.nationalcode = question.nationalcode;
            profile.tell = question.tell;
            dbprofile.Upsert(profile);
            if (fullnamechange)
                await mainserver.q(new q_rename()
                {
                    user = question.z_user,
                    fullname = question.fullname
                });
            notify(question.z_user, new n_update());
            return null;
        }
        private bool valid(string fullname)
        {
            if (fullname == null || fullname.Length < 5 || fullname.Length > 25)
                return false;
            if (fullname.Contains("  "))
                return false;
            foreach (var i in fullname)
            {
                if (i == ' ')
                    continue;
                if (!char.IsLetter(i))
                    return false;
            }
            if (fullname.First() == ' ' || fullname.Last() == ' ')
                return false;
            return true;
        }
    }
}