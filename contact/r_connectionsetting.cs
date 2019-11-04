using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact
{
    class r_connectionsetting
    {
        public string text = default;
        public e_contactsetting setting = e_contactsetting.none;
        r_connectionsetting(e_contactsetting setting, string text)
        {
            this.setting = setting;
            this.text = text;
        }
        public override string ToString()
        {
            return text;
        }
        public readonly static r_connectionsetting blocked = new r_connectionsetting(e_contactsetting.blocked, "مسدود");
        public readonly static r_connectionsetting favorite = new r_connectionsetting(e_contactsetting.favorite, "مهم");
        public readonly static r_connectionsetting none = new r_connectionsetting(e_contactsetting.none, "هیچ کدام");
        public readonly static r_connectionsetting ordinary = new r_connectionsetting(e_contactsetting.ordinary, "عادی");
        public readonly static r_connectionsetting[] list = { none, ordinary, favorite, blocked };
        public static r_connectionsetting get(e_contactsetting val)
        {
            return list.First(i => i.setting == val);
        }
    }
}