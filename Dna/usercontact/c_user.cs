using Dna.user;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.usercontact
{
    public class q_user : question
    {
        public s_user user { get; set; }
        public override e_permission z_permission => e_permission.server;
    }
}