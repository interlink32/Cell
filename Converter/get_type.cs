using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Converter
{
    class get_type
    {
        static List<Type> list = new List<Type>();
        static Assembly assembly = Assembly.GetAssembly(typeof(Dna.gene));
        public static Type GetType(string chromosome, string gene)
        {
            var dv = "Dna." + chromosome + "." + gene;
            var dv2 = list.FirstOrDefault(i => i.FullName == dv);
            if (dv2 == null)
            {
                dv2 = assembly.GetType(dv);
                if (dv2 == null)
                    throw new Exception("hdkbjdkbjjnkcjbkd");
                list.Add(dv2);
            }
            return dv2;
        }
    }
}