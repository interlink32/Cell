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
        [BsonId]
        public long chat_id { get; set; }
        public long sender { get; set; }
        public string text { get; set; }
        public DateTime time { get; set; }
        public bool delivery { get; set; }
        public s_message create()
        {
            return new s_message()
            {
                delivery = delivery,
                chat_id = chat_id,
                sender = sender,
                text = text,
                time = time
            };
        }
    }
}