using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class server<T> : client where T : request
    {
        public abstract Task<T> answer(request request);
    }
}