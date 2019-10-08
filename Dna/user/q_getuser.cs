using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_getuser : question
    {
        public string token = default;
        public class done : answer
        {
            public s_user user = default;
        }
        public class invalidtoken : answer { }
    }
}