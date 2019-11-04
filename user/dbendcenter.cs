using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user
{
    class dbendcenter
    {
        internal long userid;
        dbenduser dbenduser;
        public dbendcenter(long userid)
        {
            this.userid = userid;
            dbenduser = new dbenduser(userid);
        }
        public void close()
        {
            dbenduser.close();
        }
    }
}
