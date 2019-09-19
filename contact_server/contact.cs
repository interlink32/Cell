using Dna.contact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    public class contact
    {
        [BsonId]
        public long id { get; set; }
        public member[] members { get; set; }
        public bool any(params long[] member)
        {
            foreach (var i in member)
            {
                if (!members.Any(j => j.person == i))
                    return false;
            }
            return true;
        }
        public s_contact clone()
        {
            return new s_contact()
            {
                id = id,
                members = members.Select(i => i.clone()).ToArray()
            };
        }
    }
}