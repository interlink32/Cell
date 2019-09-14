using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_autologin : question
    {
        public double divice = 0;
        public double token = 0;
        public bool accept_notifications = false;
        public class done : answer
        {
            public long id = 0;
        }
        public class invalid_token : answer { }
    }
}