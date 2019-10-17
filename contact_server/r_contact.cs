using Dna.usercontact;
using Dna.profile;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    public class r_contact
    {
        public long id { get; set; }
        public s_profile partner { get; set; }
        public e_contactsetting mysetting { get; set; }
        public e_contactsetting partnersetting { get; set; }
        internal s_usercontact clone()
        {
            return new s_usercontact()
            {
                id = id,
                partner = partner,
                mysetting = mysetting,
                partnersetting = partnersetting
            };
        }
    }
}