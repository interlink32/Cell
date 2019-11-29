using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna.userdata
{
    public class s_contact : s_entity
    {
        public virtual e_contactsetting ownersetting { get; set; } = e_contactsetting.disconnect;
        public virtual e_contactsetting partnersetting { get; set; } = e_contactsetting.disconnect;
        public void copy(s_contact i)
        {
            ownersetting = i.ownersetting;
            partnersetting = i.partnersetting;
        }
        public override string ToString()
        {
            return id + ":owner." + ownersetting + ", partner." + partnersetting;
        }
        public bool valid => ownersetting == e_contactsetting.connect || ownersetting == e_contactsetting.favorite;
        public bool minvalid => ownersetting != e_contactsetting.blocked;
    }
}