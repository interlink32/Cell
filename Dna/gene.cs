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
        public string z_redirect = null;
        public gene()
        {
            var dv = get(GetType());
            z_gene = dv.gene;
            z_chromosome = dv.chromosom;
        }
        public string z_endchromosome => z_redirect == null ? z_chromosome : z_redirect;
        public static (string chromosom, string gene) get(Type type)
        {
            var full_name = type.FullName;
            if (full_name != "Dna.gene")
                full_name = full_name.Replace("Dna.", "");
            var dv = full_name.Split('.');
            return (dv[0], dv[1]);
        }
        static List<string> list = new List<string>();
        public static e_chromosome get(string chromosom)
        {
            if (list.Count == 0)
            {
                lock (list)
                {
                    if (list.Count == 0)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            string dv = ((e_chromosome)i).ToString();
                            if (dv == i.ToString())
                                break;
                            list.Add(dv);
                        }
                    }
                }
            }
            return (e_chromosome)list.IndexOf(chromosom);
        }
    }
}