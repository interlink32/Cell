using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class f_get_introcode : request
    {
        public chromosome chromosome = 0;
        public class done : response
        {
            public byte[] introcode = null;
        }
        public class login_required : response { }
    }
}