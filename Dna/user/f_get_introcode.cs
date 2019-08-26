using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_get_introcode : request
    {
        public string chromosome = null;
        public class done
        {
            public byte[] introcode = null;
        }
    }
}