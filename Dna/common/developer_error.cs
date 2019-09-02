using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class developer_error : answer
    {
        public developer_error(string code) { this.code = code; }
        public string code = null;
    }
}