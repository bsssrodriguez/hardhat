using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using dein.tools;
using static dein.tools.Paths;

using ct = dein.tools.Colorify.Type;

namespace HardHat {

    class Configuration {
        public static void Select() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;

            $"=".bgInfo(ct.Repeat);
            $" CONFIGURATION".bgInfo(ct.PadLeft);
            $"=".bgInfo(ct.Repeat);
            $"".fmNewLine();

            $" [P] Paths".txtMuted(ct.WriteLine);
            $"{"   [D] Development", -25}".txtPrimary();   $"{c.path.dir}".txtDefault(ct.WriteLine);
            $"{"   [B] Business"   , -25}".txtPrimary();   $"{c.path.bsn}".txtDefault(ct.WriteLine);
            $"{"   [P] Projects"   , -25}".txtPrimary();   $"{c.path.prj}".txtDefault(ct.WriteLine);
            $"{"   [F] Filter"     , -25}".txtPrimary();   $"{c.path.flt}".txtDefault(ct.WriteLine);

            $"".fmNewLine();
            $" [A] Android Path".txtMuted(ct.WriteLine);
            $"{"   [P] Project"    , -25}".txtPrimary();   $"{c.android.prj}".txtDefault(ct.WriteLine);
            $"{"   [B] Build"      , -25}".txtPrimary();   $"{c.android.bld}".txtDefault(ct.WriteLine);
            $"{"   [E] Extension"  , -25}".txtPrimary();   $"{c.android.ext}".txtDefault(ct.WriteLine);
            $"{"   [C] Compact"    , -25}".txtPrimary();   $"{c.android.cmp}".txtDefault(ct.WriteLine);
            $"{"   [F] Filter"     , -25}".txtPrimary();   $"{string.Join(",", c.android.flt)}".txtDefault(ct.WriteLine);

            $"".fmNewLine();
            $" [G] Gulp Path".txtMuted(ct.WriteLine);
            $"{"   [S] Server"     , -25}".txtPrimary();   $"{c.gulp.srv}".txtDefault(ct.WriteLine);
            $"{"   [E] Extension"  , -25}".txtPrimary();   $"{c.gulp.ext}".txtDefault(ct.WriteLine);

            $"".fmNewLine();
            $" [V] VPN".txtMuted(ct.WriteLine);
            $"{"   [S] Sitename"   , -25}".txtPrimary();   $"{c.vpn.snm}".txtDefault(ct.WriteLine);

            $"".fmNewLine();
            $"{"[EMPTY] Save", 82}".txtSuccess(ct.WriteLine);
            
            $"".fmNewLine();
            $"=".bgInfo(ct.Repeat);
            $"".fmNewLine();

            $"{" Make your choice:", -25}".txtInfo();
            string opt = Console.ReadLine();
            cp.mnu.sel = $"c>{opt?.ToLower()}";

            switch (opt?.ToLower())
            {
                case "pd":
                    Configuration.PathDevelopment();
                    break;
                case "pb":
                    Configuration.PathBusiness();
                    break;
                case "pp":
                    Configuration.PathProjects();
                    break;
                case "pf":
                    Configuration.PathFilter();
                    break;
                case "ap":
                    Configuration.AndroidProject();
                    break;
                case "ab":
                    Configuration.AndroidBuild();
                    break;
                case "ae":
                    Configuration.AndroidExtension();
                    break;
                case "ac":
                    Configuration.AndroidCompact();
                    break;
                case "af":
                    Configuration.AndroidFilter();
                    break;
                case "gs":
                    Configuration.GulpServer();
                    break;
                case "ge":
                    Configuration.GulpExtension();
                    break;
                case "vs":
                    Configuration.SiteName();
                    break;
                case "":
                    Settings.Save(c);
                    Menu.Start();
                    break;
                default:
                    cp.mnu.sel = "c";
                    break;
            }
            
            Message.Error();
        }

        #region Paths
        public static void PathDevelopment() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > PATH DEVELOPMENT".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Write main Development path.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at end.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    string dirPath = Paths.Combine(opt);
                    if (!Directory.Exists(dirPath))
                    {
                        StringBuilder msg = new StringBuilder();
                        msg.Append($" Path not found: {Environment.NewLine}");
                        msg.Append($" '{dirPath}'{Environment.NewLine}");

                        Message.Error(
                            msg: msg.ToString(), 
                            replace: true
                        );
                    } else {
                        c.path.dir = $"{opt}";
                        c.path.bsn = "";
                    }
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void PathBusiness() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > PATH BUSINESS".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                string dirPath = Paths.Combine(c.path.dir);

                if (!Directory.Exists(dirPath)){
                    StringBuilder msg = new StringBuilder();
                    msg.Append($" Path not found:{Environment.NewLine}");
                    msg.Append($" '{dirPath}'{Environment.NewLine}");
                    msg.Append(Environment.NewLine);
                    msg.Append(" Please review your configuration file.");

                    Message.Critical(
                        msg: msg.ToString()
                    );
                }

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(dirPath));
                if (dirs.Count < 1)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.Append($" There is no business in current location:{Environment.NewLine}");
                    msg.Append($" '{dirPath}'");
                    
                    Message.Alert(msg.ToString());
                }
                var i = 1;
                foreach (var dir in dirs)
                {
                    string d = dir.Slash();
                    $" {i, 2}] {d.Substring(d.LastIndexOf("/") + 1)}".txtPrimary(ct.WriteLine);
                    i++;
                }

                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"{" Make your choice:", -25}".txtInfo();
                string opt = Console.ReadLine();

                if (!String.IsNullOrEmpty(opt))
                {
                    Validation.Range(opt, 1, dirs.Count);
                    
                    var sel = dirs[Convert.ToInt32(opt) - 1].Slash();
                    c.path.bsn = sel.Substring(sel.LastIndexOf("/") + 1);
                }

                Select();
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Message.Critical(
                    msg: $" {UAEx.Message}"
                );
            }
            catch (PathTooLongException PathEx)
            {
                Message.Critical(
                    msg: $" {PathEx.Message}"
                );
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void PathProjects() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > PATH PROJECTS".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Projects folder inside Business path.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at start or end.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    string dirPath = Paths.Combine(c.path.dir, c.path.bsn, opt);
                    if (!Directory.Exists(dirPath))
                    {
                        StringBuilder msg = new StringBuilder();
                        msg.Append($" Path not found: {Environment.NewLine}");
                        msg.Append($" '{dirPath}'{Environment.NewLine}");

                        Message.Error(
                            msg: msg.ToString(), 
                            replace: true
                        );
                    } else {
                        c.path.prj = $"{opt}";
                    }
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void PathFilter() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > PATH FILTER".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Filter for folders inside Projects path.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at start or end.".txtPrimary(ct.WriteLine);
                $" Can use wildcard.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.path.flt = $"{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }
        #endregion

        #region Android
        public static void AndroidProject() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > ANDROID PROJECT".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Android folder inside selected project path.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at start or end.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    string dirPath = Paths.Combine(c.path.dir, c.path.bsn, opt);
                    if (!Directory.Exists(dirPath))
                    {
                        StringBuilder msg = new StringBuilder();
                        msg.Append($" Path not found: {Environment.NewLine}");
                        msg.Append($" '{dirPath}'{Environment.NewLine}");

                        Message.Error(
                            msg: msg.ToString(), 
                            replace: true
                        );
                    } else {
                        c.android.prj = $"{opt}";
                    }
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void AndroidBuild() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > ANDROID BUILD".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Build path inside Android Project folder.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at start or end.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.android.bld = $"{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void AndroidExtension() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > ANDROID EXTENSION".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" File extension inside Build folder.".txtPrimary(ct.WriteLine);
                $" Don't use . (dot character) at start.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.android.ext = $".{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void AndroidCompact() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > ANDROID COMPACT".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Files path inside Selected Project to be compacted with gulp.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at start or end.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.android.cmp = $"{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void AndroidFilter() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > ANDROID FILTER".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Filter extension name to be proccessed with gulp.".txtPrimary(ct.WriteLine);
                $" List separated by , (comma character).".txtPrimary(ct.WriteLine);
                $" Don't use . (dot character) at start.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    var list = opt.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = $".{list[i]}";
                    }
                    c.android.flt = list;
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }
        #endregion

        #region Gulp
        public static void GulpServer() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > GULP SERVER".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Server path inside Gulp path.".txtPrimary(ct.WriteLine);
                $" Don't use / (slash character) at start or end.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.gulp.srv = $"{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void GulpExtension() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > GULP EXTENSION".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" File extension inside Server folder.".txtPrimary(ct.WriteLine);
                $" Don't use . (dot character) at start.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.gulp.ext = $".{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }
        #endregion

        #region VPN
        public static void SiteName() {
            Colorify.Default();
            Console.Clear();

            var c =  Program.config;
            var cp =  Program.config.personal;
            try
            {
                $"=".bgInfo(ct.Repeat);
                $" CONFIGURATION > VPN SITE NAME".bgInfo(ct.PadLeft);
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();

                $"".fmNewLine();
                $" Site name for VPN connection.".txtPrimary(ct.WriteLine);
                
                $"".fmNewLine();
                $"{"[EMPTY] Cancel", 82}".txtDanger(ct.WriteLine);
                
                $"".fmNewLine();
                $"=".bgInfo(ct.Repeat);
                $"".fmNewLine();
            
                $"{" Make your choice: ", -25}".txtInfo();
                string opt = Console.ReadLine();
                if (!String.IsNullOrEmpty(opt))
                {
                    c.vpn.snm = $"{opt}";
                }

                Menu.Status();
                Select();
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }
        #endregion
    }
}