using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class error : answer
    {
        public string code = null;
        public static error create(string v)
        {
            return new error() { code = v };
        }
    }
}