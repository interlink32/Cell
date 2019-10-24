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
    class rename : myservice<q_renameuser>
    {
        public async override Task<answer> getanswer(q_renameuser question)
        {
            await Task.CompletedTask;
            var user = dbuser.FindOne(i => i.id == question.z_user);
            if (user.fullname != question.fullname)
            {
                if (!valid(question.fullname))
                    return new developererror() { code = "kjdjbdnbkdmsnbjvndmnbnd" };
                if (dbuser.Exists(i => i.fullname == question.fullname && i.id != user.id))
                    return new q_renameuser.done() { p_duplicate = true };
                user.fullname = question.fullname;
                dbuser.Update(user);
                dbdiff.Delete(i => i.userid == user.id);
                dbdiff.Insert(new r_difference()
                {
                    state = r_diffstate.update,
                    userid = user.id
                });
                notify(e_chromosome.profile);
                notify(question.z_user);
            }
            return new q_renameuser.done();
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