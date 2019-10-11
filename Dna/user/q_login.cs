using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_login : question
    {
        public string token = default;
        public bool notifyconnection = default;
        public q_login() { }
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public s_user user = default;
        }
        public class invalidtoken : answer { }
    }
}