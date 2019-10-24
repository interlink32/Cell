using controllibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profile
{
    class app : panel
    {
        public override string text => "پروفایل";

        public override uiapp createapp(long userid)
        {
            return new body(userid);
        }
    }
}