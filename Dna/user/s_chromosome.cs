using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Dna.user
{
    public class s_chromosome
    {
        public e_chromosome type = 0;
        public string endpoint = null;
        public byte[] publickey = null;
        public override string ToString()
        {
            return type.ToString() + " , " + endpoint;
        }
    }
}