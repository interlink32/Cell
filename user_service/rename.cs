using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class rename : myservice<q_rename>
    {
        public async override Task<answer> getanswer(q_rename question)
        {
            await Task.CompletedTask;
            var user = dbuser.FindOne(i => i.id == question.z_user);
            if (user.fullname != question.fullname)
            {
                if (!valid(question.fullname))
                    return new developererror() { code = "kjdjbdnbkdmsnbjvndmnbnd" };
                if (dbuser.Exists(i => i.fullname == question.fullname && i.id != user.id))
                    return new q_rename.duplicate();
                user.fullname = question.fullname;
                dbuser.Update(user);
                notify(question.z_user);
                notify(e_chromosome.profile);
            }
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