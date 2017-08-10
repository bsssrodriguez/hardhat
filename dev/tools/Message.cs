using System;
using System.Net;
using System.Net.Sockets;
using HardHat;
using ct = dein.tools.Colorify.Type;

namespace dein.tools
{
    class Message
    {
        public static void Critical(string msg = null){
            var cp =  Program.config.personal;
            cp.mnu.sel = "m";
            Error(msg, !String.IsNullOrEmpty(msg));
        }


        public static void Error(string msg = null, bool replace = false, bool exit = false) {
            Colorify.Default();
            Console.Clear();

            try
            {
                $"=".bgDanger(ct.Repeat);
                $" ERROR".bgDanger(ct.PadLeft);
                $"=".bgDanger(ct.Repeat);
                $"".fmNewLine();

                if (!replace)
                {
                    $" Invalid option, please try again.".txtDefault(ct.WriteLine);
                }
                if (!String.IsNullOrEmpty(msg))
                {
                    $" {msg}".txtDefault(ct.Shell);
                }

                $"".fmNewLine();
                $"=".bgDanger(ct.Repeat);
                $"".fmNewLine();

                $" Press [Any] key to continue...".txtDanger();
                Console.ReadKey();

                if (!exit)
                {
                    Menu.Route();
                } else {
                    Program.Exit();
                }
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static void Alert(string msg, bool exit = false) {
            Colorify.Default();
            Console.Clear();
            
            try
            {
                $"=".bgWarning(ct.Repeat);
                $" WARNING".bgWarning(ct.PadLeft);
                $"=".bgWarning(ct.Repeat);
                $"".fmNewLine();

                $" {msg}".txtDefault(ct.Shell);

                $"".fmNewLine();
                $"=".bgWarning(ct.Repeat);
                $"".fmNewLine();

                $" Press [Any] key to continue...".txtWarning();
                Console.ReadKey();

                if (!exit)
                {
                    Menu.Start();
                } else {
                    Program.Exit();
                }
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
        }

        public static bool Confirmation(string msg) {
            Colorify.Default();
            Console.Clear();
            
            bool opt_cnf = false;        
            try
            {
                $"=".bgWarning(ct.Repeat);
                $" WARNING".bgWarning(ct.PadLeft);
                $"=".bgWarning(ct.Repeat);
                $"".fmNewLine();

                $" {msg}".txtDefault(ct.Shell);

                $"".fmNewLine();
                $"=".bgWarning(ct.Repeat);
                $"".fmNewLine();

                $" [Y] Yes or [N] No: ".txtWarning();
                string opt = Console.ReadLine();
                switch (opt?.ToLower())
                {
                    case "y":
                        opt_cnf = true;
                        break;
                    case "n":
                        opt_cnf = false;
                        break;
                    default:
                        Message.Error();
                        break;
                }
            }
            catch (Exception Ex){
                Message.Critical(
                    msg: $" {Ex.Message}"
                );
            }
            return opt_cnf;
        }
    }
}