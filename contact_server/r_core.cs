using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    public class r_core
    {
        public long id { get; set; }
        public long[] memebers { get; set; }
        public long[] blockers { get; set; }
        public bool blocked => blockers == null || blockers.Length == 0;
        public void block(long user, bool value)
        {
            List<long> l = new List<long>();
            if (blockers != null)
                l.AddRange(blockers);
            if (value)
            {
                if (l.Contains(user))
                    l.Add(user);
            }
            else
                l.Remove(user);
            blockers = l.ToArray();
        }
        public bool include(params long[] users)
        {
            foreach (var i in users)
                if (!memebers.Contains(i))
                    return false;
            return true;
        }
    }
}