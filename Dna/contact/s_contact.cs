using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna.contact
{
    public class s_contact
    {
        public long id = default;
        public s_member[] members = default;
        public long another(long user)
        {
            if (members.Length != 2)
                throw new Exception("kgjchhdhvhxbdhvxgvbdbvh");
            if (members[0].person == user)
                return members[1].person;
            else
                return members[0].person;
        }
        public bool included(long user)
        {
            return members.Any(i => i.person == user);
        }
    }
}