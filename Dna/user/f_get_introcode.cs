using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_get_introcode : question
    {
        public class done : answer
        {
            public byte[] introcode = null;
        }
    }
}