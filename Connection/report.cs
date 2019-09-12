using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Connection
{
    public static class report
    {
        public delegate void answer_time(long totalMilliseconds, string chromosome);
        public static answer_time answer_time_e;
        public delegate void error(core core, string message);
        public static error error_e = null;
        public static int cunter = 0;
    }
}