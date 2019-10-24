using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_loadprofile : question
    {
        public long id = default;
        public class done : answer
        {
            public s_profile profile = default;
        }
    }
}