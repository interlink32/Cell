using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public class gene
    {
        public string z_gene = null;
        public string z_chromosome = null;
        public e_chromosome? z_redirect = null;
        public gene()
        {
            var dv = get(GetType());
            z_gene = dv.gene;
            z_chromosome = dv.chromosom;
        }
        public static (string chromosom, string gene) get(Type type)
        {
            var full_name = type.FullName;
            if (full_name != "Dna.gene")
                full_name = full_name.Replace("Dna.", "");
            var dv = full_name.Split('.');
            return (dv[0], dv[1]);
        }
    }
}