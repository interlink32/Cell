using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_login : question
    {
        public string userid = null;
        public string password = null;
        public double divce = 0;
        public class done : answer
        {
            public long id = 0;
            public double token = 0;
        }
        public class invalid : answer { }
    }
}