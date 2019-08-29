using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_login : request
    {
        public string userid = null;
        public string password = null;
        public class done : response
        {
            public long id = 0;
        }
        public class invalid : response { }
    }
}