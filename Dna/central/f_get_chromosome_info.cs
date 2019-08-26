using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.central
{
    public class f_get_chromosome_info : request
    {
        public class done : response
        {
            public s_chromosome_info[] chromosome_infos = null;
        }
    }
}