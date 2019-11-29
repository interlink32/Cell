using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace localdb
{
    public static class alldb
    {
        static List<syncdb<s_fulluser>> dbuserlst = new List<syncdb<s_fulluser>>();
        public static syncdb<s_fulluser> dbuser(long userid)
        {
            lock (dbuserlst)
            {
                var dv = dbuserlst.FirstOrDefault(i => i.userid == userid);
                if (dv == null)
                {
                    dv = new syncdb<s_fulluser>(userid);
                    dbuserlst.Add(dv);
                }
                return dv;
            }
        }
    }
}