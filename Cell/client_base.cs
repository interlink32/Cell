using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public abstract class client_base
    {
        public abstract Task<response> question(request request);
    }
}