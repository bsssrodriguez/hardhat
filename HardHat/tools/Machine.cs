using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

using ct = dein.tools.Colorify.Type;
using HardHat;

namespace dein.tools
{
    static class Machine 
    {
        public static string User(){
            return Env.Get("USERNAME") ?? Env.Get("USER");
        }

        public static string Domain(){
            return Env.Get("USERDOMAIN") ?? Env.Get("HOSTNAME");
        }
    }

    static partial class Env {
        public static string Get(string value) {
            return Environment.GetEnvironmentVariable(value);
        }

        public static void Set(string name, string value) {
            Environment.SetEnvironmentVariable(name, value);
        }

        public static bool Check(string value){
            string env = Env.Get(value);
            return !String.IsNullOrEmpty(env);
        }
    }
}