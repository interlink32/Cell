using Dna.profile;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna.usercontact
{
    public class s_usercontact : s_contact
    {
        public e_contactsetting mysetting { get; set; }
        public e_contactsetting partnersetting { get; set; }
        public void copy(s_usercontact i)
        {
            mysetting = i.mysetting;
            partnersetting = i.partnersetting;
        }
        public override string ToString()
        {
            return partnerid + ":My." + mysetting + ",partner." + partnersetting;
        }
        public bool valid => mysetting == e_contactsetting.ordinary || mysetting == e_contactsetting.favorite;
    }
}