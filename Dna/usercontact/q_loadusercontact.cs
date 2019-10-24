using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.usercontact
{
    public class q_loadusercontact : question
    {
        public long[] partnerids = default;
        public class done : answer
        {
            public s_usercontact[] usercontacts = default;
        }
    }
}