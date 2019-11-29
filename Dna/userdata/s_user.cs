using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.userdata
{
    public class s_user : s_entity
    {
        public string fullname { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public string tell { get; set; }
        public string nationalcode { get; set; }
        public e_nature nature { get; set; }
        public override string ToString()
        {
            return fullname;
        }
        public override void update(long owner, s_entity entity)
        {
            var dv = entity as s_user;
            id = dv.id;
            fullname = dv.fullname;
            description = dv.description;
            city = dv.city;
            tell = dv.tell;
            nationalcode = dv.nationalcode;
            nature = dv.nature;
        }
    }
}