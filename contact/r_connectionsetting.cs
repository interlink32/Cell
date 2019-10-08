using Dna.contact;
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
        public e_connectionsetting setting = e_connectionsetting.none;
        r_connectionsetting(e_connectionsetting setting, string text)
        {
            this.setting = setting;
            this.text = text;
        }
        public override string ToString()
        {
            return text;
        }
        public readonly static r_connectionsetting any = new r_connectionsetting(e_connectionsetting.any, "Any");
        public readonly static r_connectionsetting blocked = new r_connectionsetting(e_connectionsetting.blocked, "Blocked");
        public readonly static r_connectionsetting favorite = new r_connectionsetting(e_connectionsetting.favorite, "Favorite");
        public readonly static r_connectionsetting none = new r_connectionsetting(e_connectionsetting.none, "None");
        public readonly static r_connectionsetting ordinary = new r_connectionsetting(e_connectionsetting.ordinary, "Ordinary");
        public readonly static r_connectionsetting[] list = { none, ordinary, favorite, blocked, any };
        public static r_connectionsetting get(e_connectionsetting val)
        {
            return list.First(i => i.setting == val);
        }
    }
}