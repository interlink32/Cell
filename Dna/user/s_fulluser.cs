using Dna.profile;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class s_fulluser
    {
        public long id { get; set; }
        public s_profile profile { get; set; }
        public s_usercontact usercontact { get; set; }
        public void copy(s_fulluser fulluser)
        {
            profile.copy(fulluser.profile);
            usercontact.copy(fulluser.usercontact);
        }
    }
}
