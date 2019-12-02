using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.file
{
    class q_getfileinfo
    {
        public s_fileid fileid = default;
        public class done : answer
        {
            public s_fileinfo fileinfo = default;
        }
    }
}