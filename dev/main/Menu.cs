using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using dein.tools;

using ct = dein.tools.Colorify.Type;

namespace HardHat {
    
    public static class Menu {

        private static Config _c { get; set; }
        private static PersonalConfiguration _cp { get; set; }
        private static IEnumerable<Option> _m { get; set; }

        static Menu()
        {
            _c  = Program.config;
            _cp = Program.config.personal;
            _m  = Options.list;
        }

        public static void Status(string sel = null){
            try
            {
                _cp.ipl = Network.GetLocalIPAddress();

                if (!String.IsNullOrEmpty(sel))
                {
                    _cp.mnu.sel = sel;
                }

                string dirPath = Paths.Combine(_c.path.dir, _c.path.bsn, _c.path.prj, _cp.spr ?? "");
                // Project
                if (!Directory.Exists(dirPath))
                {
                    _cp.spr = "";
                }
                Options.Valid("p" , true);
                string filePath = Paths.Combine(dirPath, _c.android.prj, _c.android.bld, _cp.sfl ?? "");
                if (!File.Exists(filePath))
                {
                    _cp.sfl = "";
                }
                Options.Valid("pf", !Strings.SomeNullOrEmpty(_cp.spr));
                Options.Valid("pi", !Strings.SomeNullOrEmpty(_cp.spr, _cp.sfl));
                Options.Valid("pd", !Strings.SomeNullOrEmpty(_cp.spr, _cp.sfl));
                Options.Valid("pp", !Strings.SomeNullOrEmpty(_cp.spr, _cp.sfl));
                Options.Valid("ps", !Strings.SomeNullOrEmpty(_cp.spr, _cp.sfl));
                Options.Valid("pv", !Strings.SomeNullOrEmpty(_cp.spr, _cp.sfl));
                // Version Control System
                _cp.mnu.v_bnc = "";
                if (!String.IsNullOrEmpty(_cp.spr)){
                    string bnc = Git.CmdBranch(dirPath);
                    if (!String.IsNullOrEmpty(bnc))
                    {
                        _cp.mnu.v_bnc = $"git:{Git.CmdBranch(dirPath)}";
                    } 
                }
                Options.Valid("v"   , Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.mnu.v_bnc));
                Options.Valid("vd"  , Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.mnu.v_bnc));
                Options.Valid("vp"  , Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.mnu.v_bnc));
                Options.Valid("vr"  , Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.mnu.v_bnc));
                Options.Valid("vd+p", Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.mnu.v_bnc));
                Options.Valid("vr+p", Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.mnu.v_bnc));
                // Sonar
                StringBuilder s_cnf = new StringBuilder();
                s_cnf.Append($"{_cp.snr.ptc}://");
                if (!String.IsNullOrEmpty(_cp.snr.dmn))
                {
                    s_cnf.Append($"{_cp.snr.dmn}");
                }
                if (!String.IsNullOrEmpty(_cp.snr.prt))
                {
                    s_cnf.Append($":{_cp.snr.prt}");
                }
                if (!String.IsNullOrEmpty(_cp.snr.ipt))
                {
                    s_cnf.Append($"/{_cp.snr.ipt}");
                }
                _cp.mnu.s_cnf = s_cnf.ToString();
                Options.Valid("s" , Variables.Valid("sq"));
                Options.Valid("sq", Variables.Valid("sq"));
                Options.Valid("ss", Variables.Valid("ss") && !Strings.SomeNullOrEmpty(_cp.spr));
                Options.Valid("sb", Variables.Valid("sb") && !Strings.SomeNullOrEmpty(_cp.snr.ptc, _cp.snr.dmn, _cp.mnu.s_cnf));
                // Gulp
                StringBuilder g_cnf = new StringBuilder();
                g_cnf.Append($"{_cp.gbs.ptc}://");
                if (!String.IsNullOrEmpty(_cp.gbs.dmn))
                {
                    g_cnf.Append(_cp.gbs.dmn);
                }
                g_cnf.Append(Section.FlavorName(_cp.gbs.flv));
                g_cnf.Append(_cp.gbs.srv);
                if (!String.IsNullOrEmpty(_cp.gbs.ipt))
                {
                    g_cnf.Append($"/{_cp.gbs.ipt}");
                }
                g_cnf.Append(_cp.gbs.syn ? "+Sync" : "");
                _cp.mnu.g_cnf = g_cnf.ToString();
                Options.Valid("g"  , Variables.Valid("gp"));
                Options.Valid("g>i", Variables.Valid("gp"));
                Options.Valid("g>d", Variables.Valid("gp"));
                Options.Valid("g>f", Variables.Valid("gp"));
                Options.Valid("g>n", Variables.Valid("gp"));
                Options.Valid("g>s", Variables.Valid("gp"));
                Options.Valid("g>p", Variables.Valid("gp"));
                Options.Valid("gu" , Variables.Valid("gp") && !Strings.SomeNullOrEmpty(_cp.spr));
                Options.Valid("gr" , Variables.Valid("gp") && !Strings.SomeNullOrEmpty(_cp.spr));
                Options.Valid("gs" , Variables.Valid("gp") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.gbs.dmn, _cp.mnu.g_cnf));
                // Build
                StringBuilder b_cnf = new StringBuilder();
                b_cnf.Append(_cp.gdl.dmn ?? "");
                b_cnf.Append(Section.FlavorName(_cp.gdl.flv));
                b_cnf.Append(Section.ModeName(_cp.gdl.mde));
                _cp.mnu.b_cnf = b_cnf.ToString();
                Options.Valid("b"  , Variables.Valid("gh"));
                Options.Valid("b>d", Variables.Valid("gh"));
                Options.Valid("b>f", Variables.Valid("gh"));
                Options.Valid("b>m", Variables.Valid("gh"));
                Options.Valid("bp" , Variables.Valid("gp") && !Strings.SomeNullOrEmpty(_cp.spr));
                Options.Valid("bc" , Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr));
                Options.Valid("bg" , Variables.Valid("gh") && !Strings.SomeNullOrEmpty(_cp.spr, _cp.gdl.mde, _cp.gdl.flv, _cp.mnu.b_cnf));
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void Start() {
            Colorify.Default();
            Console.Clear();

            string name = Assembly.GetEntryAssembly().GetName().Name.ToUpper().ToString();
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            Section.Header($" {name} # {version}|{_cp.hst} : {_cp.ipl} ");

            Status("m");

            Project();
            Vcs();
            Sonar();
            Gulp();
            Build();
            Adb();
            Footer();

            Section.HorizontalRule();

            $"{" Make your choice:", -25}".txtInfo();
            string opt = Console.ReadLine();
            Route(opt);
        }

        public static void Project() {
            if (String.IsNullOrEmpty(_cp.spr))
            {
                $" [P] Select Project".txtPrimary(ct.WriteLine);
            } else {
                $"{" [P] Selected Project:", -25}".txtPrimary();
                $"{_cp.spr}".txtDefault(ct.WriteLine);
            }
            
            if (String.IsNullOrEmpty(_cp.spr))
            {
                $"   [F] Select File".txtStatus(ct.WriteLine,   Options.Valid("pf"));
            } else {
                $"{"   [F] Selected File:", -25}".txtPrimary();
                $"{_cp.sfl}".txtDefault(ct.WriteLine);
            }

            $"{"   [I] Install" , -17}".txtStatus(ct.Write,     Options.Valid("pi"));
            $"{"[D] Duplicate"  , -17}".txtStatus(ct.Write,     Options.Valid("pd"));
            $"{"[P] Path"       , -17}".txtStatus(ct.Write,     Options.Valid("pp"));
            $"{"[S] Signer"     , -17}".txtStatus(ct.Write,     Options.Valid("ps"));
            $"{"[V] Values"     , -17}".txtStatus(ct.WriteLine, Options.Valid("pv"));

            $"".fmNewLine();
        }

        public static void Vcs() {
            if (Options.Valid("v"))
            {
                $" [V] VCS".txtMuted(ct.WriteLine);
            } else {
                $"{" [V] VCS:", -25}".txtMuted(ct.Write);
                $"{_cp.mnu.v_bnc}".txtDefault(ct.WriteLine);
            }
            $"{"   [D] Discard" , -34}".txtStatus(ct.Write,     Options.Valid("vd"));
            $"{"[P] Pull"       , -34}".txtStatus(ct.Write,     Options.Valid("vp"));
            $"{"[R] Reset"      , -17}".txtStatus(ct.WriteLine, Options.Valid("vr"));
            $"".fmNewLine();
        }

        public static void Sonar() {
            
            if (String.IsNullOrEmpty(_cp.mnu.s_cnf))
            {
                $" [S] Sonar".txtStatus(ct.WriteLine,           Options.Valid("s"));
            } else {
                $"{" [G] Sonar:", -25}".txtStatus(ct.Write,     Options.Valid("s"));
                $"{_cp.mnu.s_cnf}".txtDefault(ct.WriteLine);    
            }
            $"{"   [Q] Qube"   , -34}".txtStatus(ct.Write,      Options.Valid("sq"));
            $"{"[S] Scanner"   , -34}".txtStatus(ct.Write,      Options.Valid("ss"));
            $"{"[B] Browse"    , -17}".txtStatus(ct.WriteLine,  Options.Valid("sb"));
            $"".fmNewLine();
        }

        public static void Gulp() {
            if (String.IsNullOrEmpty(_cp.mnu.g_cnf))
            {
                $" [G] Gulp".txtStatus(ct.WriteLine,            Options.Valid("g"));
            } else {
                $"{" [G] Gulp:", -25}".txtStatus(ct.Write,      Options.Valid("g"));
                $"{_cp.mnu.g_cnf}".txtDefault(ct.WriteLine);    
            }
            $"{"   [U] Uglify" , -34}".txtStatus(ct.Write,      Options.Valid("gu"));
            $"{"[R] Revert"    , -34}".txtStatus(ct.Write,      Options.Valid("gr"));
            $"{"[S] Server"    , -17}".txtStatus(ct.WriteLine,  Options.Valid("gs"));
            $"".fmNewLine();
        }

        public static void Build(){
            if (String.IsNullOrEmpty(_cp.mnu.b_cnf))
            {
                $" [B] Build".txtStatus(ct.WriteLine,               Options.Valid("b"));
            } else {
                $"{" [B] Build:"    , -25}".txtStatus(ct.Write,         Options.Valid("b"));
                $"{_cp.mnu.b_cnf}".txtDefault(ct.WriteLine);
            }
            $"{"   [P] Properties"  , -34}".txtStatus(ct.Write,      Options.Valid("bp"));
            $"{"[C] Clean"          , -34}".txtStatus(ct.Write,      Options.Valid("bc"));
            $"{"[G] Gradle"         , -17}".txtStatus(ct.WriteLine,  Options.Valid("bg"));
            $"".fmNewLine();
        }

        public static void Adb(){
            if (String.IsNullOrEmpty(_cp.adb.dvc))
            {
                $" [A] ADB".txtMuted(ct.WriteLine);
            } else {
                $"{" [A] ADB:"          , -25}".txtMuted();
                $"{_cp.adb.dvc}".txtDefault(ct.WriteLine);
            }
            $"{"   [D] Devices"         , -34}".txtPrimary(ct.Write);
            if (!_cp.adb.wst)
            {
                $"{"[W] WiFi Connect"   , -34}".txtPrimary(ct.Write);
            } else {
                $"{"[W] WiFi Disconnect", -34}".txtPrimary(ct.Write);
            }
            $"{"[R] Restart"            , -17}".txtPrimary(ct.WriteLine);
            $"".fmNewLine();
        }

        public static void Footer(){
            $"".fmNewLine();
            $"{" [C] Config"        , -17}".txtInfo();
            $"{"[I] Info"           , -17}".txtInfo();
            $"{"[E] Environment"    , -34}".txtInfo();
            $"{"[X] Exit"           , -17}".txtDanger(ct.WriteLine);
        }

        public static void Route(string sel = "m", string dfl = "m") {
            _cp.mnu.sel = sel?.ToLower();
            if (!String.IsNullOrEmpty(_cp.mnu.sel)){
                if (Options.Valid(_cp.mnu.sel))
                {
                    Action act = Options.Action(_cp.mnu.sel, dfl);
                    _cp.mnu.sel = dfl;
                    act.Invoke();
                } else {
                    Message.Critical();
                }
            } else {
                Menu.Start();
            }
        }
    }
}