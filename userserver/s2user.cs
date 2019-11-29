using Dna;
using Dna.user;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    public class s2user : s_user
    {
        public string callerid { get; set; }
        public string activecode { get; set; }
        public bool general { get; set; }
        public bool active { get; set; }
        public string token { get; set; }
        internal s_user clone()
        {
            return new s_user()
            {
                id = id,
                fullname = fullname,
                city = city,
                description = description,
                nationalcode = nationalcode,
                nature = nature,
                tell = tell
            };
        }
        public override string ToString()
        {
            return id + " : " + callerid;
        }
        public override void update(long owner, s_entity entity)
        {
            var dv = entity as s_user;
            city = dv.city;
            description = dv.description;
            fullname = dv.fullname;
            nationalcode = dv.nationalcode;
            nature = dv.nature;
            tell = dv.tell;
        }
    }
}