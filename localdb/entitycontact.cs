using Dna;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class entitycontact<entity, contact> where entity : s_entity where contact : s_contact
    {
        public long partnerid { get; set; }
        public entity partner { get; set; }
        public contact Contact { get; set; }
    }
}