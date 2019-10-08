using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class q
    {
        internal static client client = null;
        public async static Task<answer> get(question question)
        {
            return await client.question(question);
        }
    }
}