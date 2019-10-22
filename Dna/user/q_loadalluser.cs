using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_loadalluser : question
    {
        public int skep = default;
        public string fullnamefilter = default;
        public long[] idfilter = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public s_user[] users = default;
        }
    }
}