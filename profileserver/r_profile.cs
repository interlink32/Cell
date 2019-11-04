using Dna;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class r_profile : s_entity
    {
        public string fullname { get; set; }
        public string nationalcode { get; set; }
        public string tell { get; set; }
        public e_nature nature { get; set; }
        public string city { get; set; }
        public string description { get; set; }
        public s_profile clone()
        {
            return new s_profile()
            {
                city = city,
                fullname = fullname,
                nature = nature,
                id = id,
                nationalcode = nationalcode,
                tell = tell,
                description = description
            };
        }
    }
}