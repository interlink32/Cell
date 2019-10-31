using Dna.profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_updateprofile : question
    {
        public string nationalcode { get; set; }
        public string tell { get; set; }
        public e_nature gender { get; set; }
        public string city { get; set; }
    }
}