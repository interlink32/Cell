using Dna.profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.userinfo
{
    public class s_profile
    {
        public long id { get; set; }
        public string callerid { get; set; }
        public string fullname { get; set; }
        public DateTime birthday { get; set; }
        public e_gender gender { get; set; }
    }
}