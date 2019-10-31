using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class s_profile : s_entity
    {
        public string fullname { get; set; }
        public string nationalcode { get; set; }
        public e_nature nature { get; set; }
        public string tell { get; set; }
        public string city { get; set; }
        public string description { get; set; }
        public void copy(s_profile i)
        {
            id = i.id;
            fullname = i.fullname;
            nationalcode = i.nationalcode;
            tell = i.tell;
            nature = i.nature;
            city = i.city;
            description = i.description;
        }
    }
}