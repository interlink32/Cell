﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class s_profile
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public string nationalcode { get; set; }
        public string tell { get; set; }
        public e_gender gender { get; set; }
        public string address { get; set; }
        public void copy(s_profile i)
        {
            fullname = i.fullname;
            nationalcode = i.nationalcode;
            tell = i.tell;
            gender = i.gender;
            address = i.address;
        }
    }
}