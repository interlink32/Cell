using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna.contact
{
    public class s_contact
    {
        public long id { get; set; }
        public s_member[] members { get; set; }
        public long another(long user)
        {
            if (members.Length != 2)
                throw new Exception("kgjchhdhvhxbdhvxgvbdbvh");
            if (members[0].person == user)
                return members[1].person;
            else
                return members[0].person;
        }
        public bool included(params long[] users)
        {
            foreach (var j in users)
                if (!members.Any(i => i.person == j))
                    return false;
            return true;

        }
    }
}