using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_get_introcode : request
    {
        public class done : response
        {
            public byte[] introcode = null;
        }
    }
}