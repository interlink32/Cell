using System;
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
        public s_location location { get; set; }
        public int service { get; set; }
        public int[] Jobabilities { get; set; }
        public bool Jobstatus { get; set; }
        public bool maritalstatus { get; set; }
        public e_reliability reliability { get; set; }
        public string slogan { get; set; }
        public string address { get; set; }
        public void copy(s_profile i)
        {
            fullname = i.fullname;
            nationalcode = i.nationalcode;
            tell = i.tell;
            gender = i.gender;
            location = i.location;
            service = i.service;
            Jobabilities = i.Jobabilities;
            Jobstatus = i.Jobstatus;
            maritalstatus = i.maritalstatus;
            reliability = i.reliability;
            slogan = i.slogan;
            address = i.address;
        }
    }
}