using Connection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace user
{
    class allapps
    {
        static string root = "";
        const string dll = @"interlink32/Cell/abcd/bin/Debug/netstandard2.0";
        const string exe = @"interlink32/Cell/abcd/bin/Debug";
        public static void start()
        {

            //C:\Users\mohsen\Source\Repos\interlink32\Cell\abcd\bin\Debug\
            root = Assembly.GetAssembly(typeof(allapps)).CodeBase.Replace(@"file:///", "");
            if (!root.Contains(@"Repos/interlink32/Cell"))
                return;
            root = root.Split(new string[] { @"interlink32/Cell" }, StringSplitOptions.RemoveEmptyEntries)[0];

            copystandard("Connection", ".dll");
            copystandard("Dna", ".dll");
            copystandard("localdb", ".dll");
       
            copynet("controllibrary", ".dll");
            copynet("message", ".exe");
            copynet("profile", ".exe");
            copynet("searchuser", ".exe");
            copynet("usercontact", ".exe");
            copynet("user", ".exe");
        }
        static void copystandard(string val, string pas)
        {
            var source = root + dll.Replace("abcd", val) + @"/" + val + pas;
            var end = reference.root(val + pas, "allapps");
            File.WriteAllBytes(end, File.ReadAllBytes(source));
        }
        static void copynet(string val, string pas)
        {
            var source = root + exe.Replace("abcd", val) + @"/" + val + pas;
            var end = reference.root(val + pas, "allapps");
            File.WriteAllBytes(end, File.ReadAllBytes(source));
        }
    }
}