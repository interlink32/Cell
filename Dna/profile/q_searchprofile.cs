using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_searchprofile : question
    {
        public string slogan = default;
        public string fullname = default;
        public string nationalcode = default;
        public e_nature nature = default;
        public string city = default;
        public class done : answer
        {
            public s_profile[] profiles = default;
        }
    }
}