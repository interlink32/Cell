using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_get_userid : question
    {
        public byte[] introcode = null;
        public class done : answer
        {
            public long userid = 0;
        }
        public class invalidcode : answer { }
    }
}