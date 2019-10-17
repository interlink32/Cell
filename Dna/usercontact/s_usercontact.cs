using Dna.profile;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna.usercontact
{
    public class s_usercontact
    {
        public long id { get; set; }
        public s_profile partner { get; set; }
        public e_contactsetting mysetting { get; set; }
        public e_contactsetting partnersetting { get; set; }
    }
}