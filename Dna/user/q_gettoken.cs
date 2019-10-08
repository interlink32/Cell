using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_gettoken : question
    {
        public string callerid = default;
        public string activecode = default;
        public int randomvalue = default;
        public class done : answer
        {
            public string token = default;
        }
        public class invalid : answer { }
    }
}