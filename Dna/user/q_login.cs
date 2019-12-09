using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_login : question
    {
        public string token = default;
        public bool notifier = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public long userid = default;
        }
        public class invalid : answer { }
    }
}