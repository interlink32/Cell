using Connection;
using Dna;
using Dna.common;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2upsertuser : myservice<q_upsertuser>
    {
        public async override Task<answer> getanswer(q_upsertuser question)
        {
            await Task.CompletedTask;
            var newuser = question.user;
            if (newuser == null)
                return new error() { code = "lglfkbkdjbjjbnfbjcvn" };
            if (newuser.id != question.z_user)
                return new error() { code = "lgkfkgkfjkfmgkdmgkd" };
            var olduser = db.findone(i => i.id == question.z_user);
            if (newuser.fullname != olduser.fullname)
            {
                if (!valid(newuser.fullname))
                    return new q_upsertuser.done() { error_invalidfullname = true };
                var dv = db.findone(i => i.fullname == newuser.fullname);
                if (dv != null)
                    return new q_upsertuser.done() { error_duplicate = true };
            }
            olduser.update(0, newuser);
            db.upsert(olduser, true);
            return new q_upsertuser.done();
        }
        bool valid(string fullname)
        {
            if (fullname.Length < 5)
                return false;
            if (fullname.Length > 25)
                return false;
            if (fullname.Contains("  "))
                return false;
            if (fullname.First() == ' ' || fullname.Last() == ' ')
                return false;
            foreach (var i in fullname)
            {
                if (i == ' ')
                    continue;
                if (!char.IsLetter(i))
                    return false;
            }
            return true;
        }
    }
}