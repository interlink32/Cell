using Dna;
using Dna.profile;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class dbuserclient : dbuniqueclient<s_profile, s_usercontact>
    {
        public dbuserclient(long userid) : base(userid, e_chromosome.usercontact)
        {
        }
    }
}