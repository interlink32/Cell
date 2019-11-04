using Dna.usercontact;
using Dna.profile;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;

namespace contactserver
{
    public class r_contact : s_contact
    {
        public e_contactsetting ownersetting { get; set; }
        public e_contactsetting partnersetting { get; set; }
        internal s_usercontact clone()
        {
            return new s_usercontact()
            {
                partnerid = partnerid,
                mysetting = ownersetting,
                partnersetting = partnersetting
            };
        }
    }
}