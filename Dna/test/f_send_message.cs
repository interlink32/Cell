using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.test
{
    public class f_send_message : request
    {
        public long receiver_user = 0;
        public string message = null;
        public class done : response
        {
            public bool resieved = false;
        }
    }
}