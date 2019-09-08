using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_autologin : question
    {
        public double divice = 0;
        public double token = 0;
        public class done : answer
        {
            public long id = 0;
        }
        public class invalid_token : answer { }
    }
}