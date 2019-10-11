using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class s_user
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public override string ToString()
        {
            return fullname;
        }
    }
}