using Dna.profile;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Text;

namespace classlibrary
{
    public class fulluser
    {
        public long id { get; set; }
        public s_profile profile { get; set; }
        public s_usercontact usercontact { get; set; }
        internal void copy(fulluser fulluser)
        {
            profile.copy(fulluser.profile);
            usercontact.copy(fulluser.usercontact);
        }
    }
}