using Dna;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    public class converter
    {
        public byte[] change(gene gene)
        {
            var dv = JsonConvert.SerializeObject(gene);
            var dv2 = Encoding.UTF8.GetBytes(dv);
            return dv2;
        }

        public gene change(byte[] data)
        {
            var dv = Encoding.UTF8.GetString(data);
            var dv2 = JsonConvert.DeserializeObject<gene>(dv);
            var t = get_type.GetType(dv2.z_chromosome, dv2.z_id);
            dv2 = JsonConvert.DeserializeObject(dv, t) as gene;
            return dv2;
        }
    }
}