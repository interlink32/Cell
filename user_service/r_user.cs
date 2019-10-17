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
        public string fullname { get; set; }
        public bool general { get; set; }
        internal s_user clone()
        {
            return new s_user()
            {
                id = id,
                fullname = fullname
            };
        }
    }
}