using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class r_user
    {
        public long id { get; set; }
        public string callerid { get; set; }
        public string activecode { get; set; }
        public bool general { get; set; }
        public bool active { get; set; }
        public string token { get; set; }
        public string fullname { get; set; }
        internal s_user clone()
        {
            return new s_user()
            {
                id = id,
                fullname = fullname
            };
        }
        public override string ToString()
        {
            return id + " : " + callerid;
        }
    }
}