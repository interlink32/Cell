using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.message
{
    public class s_message : s_entity2D
    {
        public e_messagestate messagestate { get; set; }
        public string text { get; set; }
        public DateTime time { get; set; }
        public bool edited { get; set; }
        public override void update(long owner, s_entity entity)
        {
            var dv = entity as s_message;
            id = dv.id;
            messagestate = dv.messagestate;
            partner = dv.partner;
            text = dv.text;
            time = dv.time;
            edited = dv.edited;
        }
    }
}