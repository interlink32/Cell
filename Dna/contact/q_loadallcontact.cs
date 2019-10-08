using Dna.user;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.contact
{
    public class q_loadallcontact : question
    {
        
        public string namefilter = default;
        public e_connectionsetting mysettingfilter = default;
        public e_connectionsetting partnersettingfilter = default;
        public class done : answer
        {
            public s_contact[] contacts = default;
        }
    }
}