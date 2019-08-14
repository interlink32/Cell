using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class gene
    {
        public string z_id { get; }
        public string z_chromosome { get; }
        public gene()
        {
            var dv = get(GetType());
            z_id = dv.gene;
            z_chromosome = dv.chromosom;
        }
        public static (string chromosom, string gene) get(Type type)
        {
            var full_name = type.FullName;
            full_name = full_name.Replace("DNA.", "");
            var dv = full_name.Split('.');
            return (dv[0], dv[1]);
        }
    }
}