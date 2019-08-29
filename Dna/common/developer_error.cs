using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class developer_error : response
    {
        public developer_error(string code) { this.code = code; }
        public string code = null;
    }
}