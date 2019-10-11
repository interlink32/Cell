using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    class r_profile
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public string nationalcode { get; set; }
        public string tell { get; set; }
        public e_gender gender { get; set; }
        public string address { get; set; }
        public s_profile clone()
        {
            return new s_profile()
            {
                address = address,
                fullname = fullname,
                gender = gender,
                id = id,
                nationalcode = nationalcode,
                tell = tell
            };
        }
    }
}