using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using static Colorify.Colors;
using dein.tools;
using ToolBox.Files;
using ToolBox.System;
using static HardHat.Program;

namespace HardHat
{

    public static class Menu
    {

        public static void Status(string sel = null)
        {
            try
            {
                _config.personal.ipAddress = Network.GetLocalIPv4();

                if (!String.IsNullOrEmpty(sel))
                {
                    _config.personal.menu.selectedOption = sel;
                }

                string dirPath = _path.Combine(_config.path.development, _config.path.workspace, _config.path.project, _config.personal.selectedProject ?? "");
                Project.Status(dirPath);
                Vcs.Status(dirPath);
                Sonar.Status();
                Gulp.Status();
                Build.Status();
            }
            catch (Exception Ex)
            {
                Exceptions.General(Ex.Message);
            }
        }

        public static void Start()
        {
            _colorify.Clear();

            string name = Assembly.GetEntryAssembly().GetName().Name.ToUpper().ToString();
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            Section.Header($" {name} # {version}|{_config.personal.hostName} : {_config.personal.ipAddress} ");

            Status("m");
            Project.Start();
            Vcs.Start();
            Sonar.Start();
            Gulp.Start();
            Build.Start();
            Adb.Start();
            Section.Footer();

            Section.HorizontalRule();

            _colorify.Write($"{" Make your choice:",-25}", txtInfo);
            string opt = Console.ReadLine();
            _colorify.Clear();
            Route(opt);
        }

        public static void Route(string sel = "m", string dfl = "m")
        {
            _config.personal.menu.selectedOption = sel?.ToLower();
            if (!String.IsNullOrEmpty(_config.personal.menu.selectedOption))
            {
                if (Options.Valid(_config.personal.menu.selectedOption))
                {
                    Action act = Options.Action(_config.personal.menu.selectedOption, dfl);
                    _config.personal.menu.selectedOption = dfl;
                    act.Invoke();
                }
                else
                {
                    Message.Error();
                }
            }
            else
            {
                Menu.Start();
            }
        }
    }
}