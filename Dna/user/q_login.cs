using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_login : question
    {
        public string user_name = default;
        public string password = default;
        public class done : answer
        {
            public long id = default;
            public double device = default;
        }
        public class invalid : answer { }
    }
}