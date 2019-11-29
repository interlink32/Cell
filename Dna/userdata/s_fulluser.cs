using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.userdata
{
    public class s_fulluser : s_entity
    {
        public long owner = default;
        public s_user user { get; set; }
        public s_contact contact { get; set; }
        public override void update(long owner, s_entity entity)
        {
            this.owner = owner;
            var dv = entity as s_fulluser;
            if (dv == null)
                throw new Exception("lblflbkfkbkfknkfkvkdkb");
            id = entity.id;
            if (dv.user != null)
                user = dv.user;
            if (dv.contact != null)
                contact = dv.contact;
        }
    }
}