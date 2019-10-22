using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class s_user : entity
    {
        public string fullname { get; set; }
        public override string ToString()
        {
            return fullname;
        }
    }
}