﻿using System;
using System.Reflection;
using System.Threading;
using dein.tools;

namespace HardHat
{
    static class Program
    {
        public static Config config  { get; set; }

        static void Main(string[] args)
        {
            bool createdMutex;
            Mutex mutex = new Mutex(true, Assembly.GetEntryAssembly().GetName().Name.ToUpper().ToString(), out createdMutex);
            if(mutex.WaitOne(TimeSpan.Zero, true)) {
                try
                {
                    //Config
                    config = Settings.Read();
                    var cp = config.personal;
                    cp.hst = System.Environment.MachineName;

                    //Check for updates
                    Env.CmdUpdate();
                    Variables.Upgrade();
                    Variables.Update();
                    Gulp.Check();
                    
                    //Window
                    if (Os.IsWindows() && (config.window.width + config.window.height) > 0)
                    {
                        Console.SetWindowSize(config.window.width, config.window.height);
                    }
                    
                    Menu.Start();
                }
                catch (Exception Ex)
                {
                    Message.Error(
                        msg: Ex.Message, 
                        replace: true, 
                        exit: true);
                    Exit();
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            } else {
                Message.Error(
                    msg: "HardHat is already running", 
                    replace: true, 
                    exit: true);
            }
        }

        public static void Exit(){
            Settings.Save(config);
            Console.Clear();
            Environment.Exit(0);
        }
    }
}