using System;
using System.Runtime.InteropServices;
using dein.tools;
using ToolBox.Bridge;
using static HardHat.Program;

namespace HardHat
{
    public static partial class Build
    {
        public static void CmdClean(string path, bool cleanCache = false)
        {
            try
            {
                string action = "clean" + (cleanCache ? "BuildCache" : "");
                _shell.Term($"gradle -p {path} {action}", Output.External);
                _config.personal.selected.path = "";
                _config.personal.selected.file = "";
                _config.personal.selected.mapping = "";
            }
            catch (Exception Ex)
            {
                Exceptions.General(Ex);
            }
        }

        public static void CmdGradle(string path, string conf, string device = null)
        {
            try
            {
                _shell.Term($"gradle -p {path} assemble{conf}", Output.External);
            }
            catch (Exception Ex)
            {
                Exceptions.General(Ex);
            }
        }

        public static void CmdRefresh(string path, string conf, string device = null)
        {
            try
            {
                _shell.Term($"gradle -p {path} --refresh-dependencies", Output.External);
            }
            catch (Exception Ex)
            {
                Exceptions.General(Ex);
            }
        }
    }
}