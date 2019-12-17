using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_getid : question
    {
        public long token = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public bool error_invalid = false;
            public long userid = default;
        }
    }
}