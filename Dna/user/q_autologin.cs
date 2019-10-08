using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_autologin : question
    {
        public string token = default;
        public class done : answer
        {
            public s_user user = default;
        }
        public class invalid_token : answer { }
    }
}