using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.file
{
    public class q_download : question
    {
        public long id = default;
        public long token = default;
        public int version = default;
        public long offset = default;
        public int length = default;
        public class done : answer
        {
            public bool error_version = false;
            public byte[] data = default;
        }
    }
}