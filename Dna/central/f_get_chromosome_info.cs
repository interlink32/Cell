using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.central
{
    public class f_get_chromosome_info : question
    {
        public class done : answer
        {
            public s_chromosome_info[] chromosome_infos = null;
        }
    }
}