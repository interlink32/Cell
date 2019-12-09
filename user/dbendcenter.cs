using Dna.message;
using Dna.userdata;
using localdb;
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
        synccenter<s_fulluser> dbuser;
        synccenter<s_message> dbmessage;
        public dbendcenter(long userid)
        {
            this.userid = userid;
            dbuser = new synccenter<s_fulluser>(userid);
            dbmessage = new synccenter<s_message>(userid);
        }
        public void close()
        {
            dbuser?.close();
            dbmessage?.close();
        }
    }
}
