using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_loadalluser : question
    {
        public int skep = default;
        public string name_filter = default;
        public long[] ids_filter = default;
        public string[] callerids_filter = default;
        public class done : answer
        {
            public s_user[] users = default;
        }
    }
}