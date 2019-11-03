using Dna;
using Dna.profile;
using Dna.usercontact;
using localdb;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class dbendreaderuser : dbendreader<s_profile, s_usercontact>
    {
        public dbendreaderuser(long userid) : base(userid)
        {
            
        }
    }
}