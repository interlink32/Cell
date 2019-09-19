using Dna.contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace contact_server
{
    public class member
    {
        public long person { get; set; }
        public state state { get; set; }
        public s_member clone()
        {
            return new s_member()
            {
                person = person,
                state = get(state)
            };
        }
        public static e_state get(state dv)
        {
            switch (dv)
            {
                case state.none:
                    return e_state.none;
                case state.opponent:
                    return e_state.opponent;
                case state.defender:
                    return e_state.defender;
            }
            throw new Exception("kgjbjfdjbjdjbjdjbjdbjdjb");
        }
    }
}