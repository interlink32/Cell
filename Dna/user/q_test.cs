﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_test : question
    {
        public byte[] input = default;
        public long outputlength = default;
        public class done : answer
        {
            public long inputlength = default;
            public byte[] output = default;
        }
    }
}