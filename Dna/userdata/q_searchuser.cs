using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.userdata
{
    public class q_searchuser : question
    {
        public string fullname { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public string tell { get; set; }
        public string nationalcode { get; set; }
        public e_nature nature { get; set; }
        public class done : answer
        {
            public s_user[] users = default;
        }
    }
}