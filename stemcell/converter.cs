using Dna;
using Dna.common;
using Dna.userdata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stemcell
{
    public class converter
    {
        public static byte[] change(gene gene)
        {
            var dv = JsonConvert.SerializeObject(gene);
            var dv2 = Encoding.UTF8.GetBytes(dv);
            return dv2;
        }
        static Type type = typeof(gene);
        public static gene change(byte[] data)
        {
            var dv = Encoding.UTF8.GetString(data);
            var dv2 = JsonConvert.DeserializeObject(dv, type) as gene;
            var t = gettype.GetType(dv2.z_chromosome, dv2.z_gene);
            dv2 = JsonConvert.DeserializeObject(dv, t) as gene;
            return dv2;
        }
    }
}