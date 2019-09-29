using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_autologin : question
    {
        public double device = 0;
        public class done : answer
        {
            public long id = 0;
        }
        public class invalid_token : answer { }
    }
}