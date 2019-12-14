using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace controllibrary
{
    public abstract class uiapp : uibase
    {
        internal long userid = default;
        internal void create_(long userid)
        {
            this.userid = userid;
            create(userid);
        }
        public abstract void create(long userid);
    }
}