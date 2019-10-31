using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_loadallprofile : question
    {
        public long[] ids = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public s_profile[] profiles = default;
        }
    }
}