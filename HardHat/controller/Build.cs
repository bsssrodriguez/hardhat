using System;
using System.Runtime.InteropServices;
using dein.tools;

namespace HardHat 
{
    public static partial class Build {
        public static void CmdClean(string path, string device = null){
            try
            {
                $"gradle -p {path} clean".Term(Output.External);
            }
            catch (Exception Ex){
                Exceptions.General(Ex.Message);
            }
        }
        public static void CmdGradle(string path, string conf, string device = null){
            try
            {
                $"gradle -p {path} clean assemble{conf}".Term(Output.External);
            }
            catch (Exception Ex){
                Exceptions.General(Ex.Message);
            }
        }
        public static void CmdRefresh(string path, string conf, string device = null){
            try
            {
                $"gradle -p {path} --refresh-dependencies".Term(Output.External);
            }
            catch (Exception Ex){
                Exceptions.General(Ex.Message);
            }
        }
    }
}