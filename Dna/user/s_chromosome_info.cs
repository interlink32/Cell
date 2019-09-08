using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Dna.user
{
    public class s_chromosome_info
    {
        public chromosome chromosome = 0;
        public string endpoint = null;
        public byte[] public_key = null;
        public override string ToString()
        {
            return chromosome.ToString() + " , " + endpoint;
        }
    }
}