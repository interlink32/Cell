using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_loadallprofile : question
    {
        public long[] ids = default;
        public class done : answer
        {
            public s_profile[] profiles = default;
        }
    }
}