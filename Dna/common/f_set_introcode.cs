using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class f_set_introcode : request
    {
        public byte[] introcode = null;
        public class done : request { }
    }
}