using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_loaduser : question
    {
        public long userid = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public s_user user = default;
        }
    }
}