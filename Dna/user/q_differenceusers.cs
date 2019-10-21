using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_differenceusers : question
    {
        public long index = default;
        public override e_permission z_permission => e_permission.server;
        public class doen : answer
        {
            public long currentindex = default;
            public s_user[] updated = default;
            public long[] deleted = default;
        }
    }
}