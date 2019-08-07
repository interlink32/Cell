using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public abstract class exchange<T> where T : request
    {
        public abstract Task<response> get(T request);
    }
}