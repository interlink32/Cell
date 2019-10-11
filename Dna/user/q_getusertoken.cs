using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_getusertoken : question
    {
        public string callerid = default;
        public string activecode = default;
        public int randomvalue = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public string token = default;
            public s_user user = default;
        }
        public class invalid : answer { }
    }
}