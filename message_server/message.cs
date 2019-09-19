using Dna.message;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace message_server
{
    public class message
    {
        public int id { get; set; }
        public long sender { get; set; }
        public string text { get; set; }
        public DateTime time { get; set; }
        public s_message create()
        {
            return new s_message()
            {
                id = id,
                sender = sender,
                text = text,
                time = time
            };
        }
    }
}