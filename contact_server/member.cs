using Dna.contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace contact_server
{
    public class member
    {
        public long person = default;
        public state state = default;
        public s_member clone()
        {
            e_state dv = default;
            switch (state)
            {
                case state.none:
                    dv = e_state.none;
                    break;
                case state.opponent:
                    dv = e_state.opponent;
                    break;
                case state.defender:
                    dv = e_state.defender;
                    break;
            }
            return new s_member()
            {
                person = person,
                state = dv
            };
        }
    }
}