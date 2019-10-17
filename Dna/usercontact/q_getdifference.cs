using Dna.user;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.usercontact
{
    public class q_getdifference : question
    {
        public long fromindex = default;
        public class doen : answer
        {
            public long currentindex = default;
            public s_usercontact[] updatedcontact = default;
            public long[] deletedcontact = default;
        }
    }
}