using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.userdata
{
    public class q_searchuser : question
    {
        public string fullname = default;
        public string description = default;
        public string city = default;
        public string tell = default;
        public string nationalcode = default;
        public e_nature? nature = default;
        public class done : answer
        {
            public s_user[] users = default;
        }
        public override void z_normalize()
        {
            nullcheck(ref fullname);
            nullcheck(ref description);
            nullcheck(ref city);
            nullcheck(ref tell);
            nullcheck(ref nationalcode);
            if (nature == e_nature.none)
                nature = null;
        }
    }
}