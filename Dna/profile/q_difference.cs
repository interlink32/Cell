using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_difference : question
    {
        public long index = default;
        public override e_permission z_permission => e_permission.server;
        public class doen : answer
        {
            public long currentindex = default;
            public s_profile[] updated = default;
            public long[] deleted = default;
        }
    }
}