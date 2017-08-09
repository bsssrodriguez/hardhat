using System.Collections.Generic;
using System.IO;
using dein.tools;
using Newtonsoft.Json;

namespace HardHat
{
    class Settings{
        public static void Save(Config config){
            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText($"config.{OS.WhatIs()}.json", json);
        }

        public static Config Read(){
            if (!File.Exists($"config.{OS.WhatIs()}.json")) {
                Config config = new Config();

                config.window = new WindowConfiguration();
                config.window.height = 30;
                config.window.width = 86;

                config.path = new PathConfiguration();
                switch (OS.WhatIs())
                {
                    case "win":
                        config.path.dir = "D:/Developer";
                        break;
                    case "mac":
                        config.path.dir = "~/Developer";
                        break;
                }
                config.path.bsn = "Contoso";
                config.path.prj = "Projects";
                config.path.flt = "_d*";
        
                config.android = new AndroidConfiguration();
                config.android.prj = "android";
                config.android.bld = "build/outputs/apk";
                config.android.ext = ".apk";
                config.android.cmp = "assets/www";
                config.android.flt = new string[] {".js",".css"};

                config.gulp = new GulpConfiguration();
                config.gulp.srv = "server";
                config.gulp.ext = ".json";

                config.vpn = new VPNConfiguration();

                config.personal = new PersonalConfiguration();
                config.personal.gdl = new BuildConfiguration();
                config.personal.gbs = new ServerConfiguration();
                config.personal.gbs.ipt = "web";
                config.personal.gbs.syn = false;
                config.personal.gbs.ptc = "http";
                config.personal.adb = new ADBConfiguration();
                config.personal.mnu = new MenuConfiguration();

                return config;
            } else {
                string json = File.ReadAllText($"config.{OS.WhatIs()}.json");
                return JsonConvert.DeserializeObject<Config>(json);
            }
        }
    } 

    class Config
    {
        public WindowConfiguration window { get; set; }
        public PathConfiguration path { get; set; }
        public AndroidConfiguration android { get; set; }
        public GulpConfiguration gulp { get; set; }
        public VPNConfiguration vpn { get; set; }
        public PersonalConfiguration personal { get; set; }
    }

    class WindowConfiguration 
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    class PathConfiguration
    {
        public string dir { get; set; }                     //Development
        public string bsn { get; set; }                     //Bussiness Name
        public string prj { get; set; }                     //Projects
        public string flt { get; set; }                     //Filter
    }

    class AndroidConfiguration {
        public string prj { get; set; }                     //Android Project Folder
        public string bld { get; set; }                     //Build Path
        public string ext { get; set; }                     //Build Extension
        public string cmp { get; set; }                     //Path to process with Gulp
        public string[] flt { get; set; }                   //Filter files to Process
    }

    class GulpConfiguration {
        public string srv { get; set; }                     //Server Folder
        public string ext { get; set; }                     //Server Extension
    }

    class VPNConfiguration {
        public string snm { get; set; }                     //Sitename
    }
    
    class PersonalConfiguration {
        public string hst { get; set; }                     //Hostname
        public string ipl { get; set; }                     //Local IP Address
        public string ipb { get; set; }                     //Local IP Address base
        public string spr { get; set; }                     //Selected Project
        public string sfl { get; set; }                     //Selected File
        public BuildConfiguration gdl { get; set; }         //Gradle Configuration
        public ServerConfiguration gbs { get; set; }        //Gradle Server
        public ADBConfiguration adb { get; set; }           //ADB Configuration
        public MenuConfiguration mnu { get; set; }          //Menu Configuration
    }

    class BuildConfiguration {
        public string mde { get; set; }                     //Mode
        public string dmn { get; set; }                     //Dimension
        public string flv { get; set; }                     //Flavor
    }

    class ServerConfiguration {
        public string dmn { get; set; }                     //Dimension
        public string flv { get; set; }                     //Flavor
        public string srv { get; set; }                     //Server
        public bool syn { get; set; }                       //Sync
        public string ptc { get; set; }                       //Protocol
        public string ipt { get; set; }
    }

    class ADBConfiguration {
        public string dvc { get; set; }                     //Device Name
        public string wip { get; set; }                     //WiFi IP
        public string wpr { get; set; }                     //WiFi Port
        public bool wst { get; set; }                       //WiFi Status
    }

    public class MenuConfiguration
    {
        public string sel { get; set; }                       //Option
        public bool p_sel { get; set; }                       //Project
        public bool f_sel { get; set; }                       //File
        public bool v_sel { get; set; }                       //Version Control System
        public string v_bnc { get; set; }                     //Current Branch
        public string g_cnf { get; set; }                     //Gulp Configuration
        public bool g_env { get; set; }                       //Gulp Environment
        public bool g_sel { get; set; }                       //Gulp
        public string b_cnf { get; set; }                     //Buils Configuration
        public bool b_env { get; set; }                       //Buils Environment
        public bool t_env { get; set; }                       //Template Environment
        public bool b_sel { get; set; }                       //Buils
        public bool v_env { get; set; }                       //VPN Environment

    }
}