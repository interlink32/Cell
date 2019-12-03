using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class textanswer : answer
    {
        public byte[] text = null;
        public textanswer()
        {
            text = new byte[300];
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = (byte)(i + 5);
            }
        }
    }
}