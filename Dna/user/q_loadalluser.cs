using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_loadalluser : question
    {
        public int skep = default;
        public string name_filter = default;
        public long[] ids_filter = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public s_user[] users = default;
        }
    }
}