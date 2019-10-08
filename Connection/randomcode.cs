using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connection
{
    public class randomcode
    {
        [BsonId]
        public string callerid { get; set; }
        public int value { get; set; }
    }
}