using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinqToDB.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SunEngine.Commons.Utils;
using SunEngine.DataSeed;
using SunEngine.Migrations;

namespace SunEngine
{
    public class Program
    {
        private const string ConfigurationArgumentName = "config:"; 
        private const string DefaultConfigurationFileName = "Config";
        
        public static string configDir;

        public static void SetUpConfigurationDirectory(IEnumerable<string> arguments)
        {
            var configurationDirectory = GetConfigurationDirectory(arguments);
            configDir = Path.GetFullPath(configurationDirectory);   
        }

        private static string GetConfigurationDirectory(IEnumerable<string> arguments)
        {
            var configurationProperty = arguments.FirstOrDefault(x => x.StartsWith(ConfigurationArgumentName));
            if (configurationProperty.IsNullOrEmpty())
            {
                Console.Write("Property for configuration wasn't set. Default configuration will be used.");
                return DefaultConfigurationFileName;
            }
            
            var configurationFileName = configurationProperty.Substring(ConfigurationArgumentName.Length).Trim();
            if (configurationFileName.IsNullOrEmpty())
            {
                Console.Write("Property for configuration was empty or blank. Default configuration will be used.");
                return DefaultConfigurationFileName;
            }

            Console.Write($"Configuration file {configurationFileName} will be used.");
            return configurationFileName;
        }
        
        public static void Main(string[] args)
        {
            SetUpConfigurationDirectory(args);

            if (args.Any(x => x == "help"))
                InfoPrinter.PrintHelp();

            else if (args.Any(x => x == "server"))
                RunServer(args);

            else if (args.Any(x => x == "version"))
                InfoPrinter.PrintVersion();

            else if(args.Any(x => x == "migrate" || x == "init" || x == "seed"))
            {
                if (args.Any(x => x == "migrate"))
                    new MainMigrator(configDir).Migrate();

                if (args.Any(x => x == "init"))
                    new MainSeeder(configDir).SeedInitialize();

                if (args.Any(x => x.StartsWith("seed")))
                    new MainSeeder(configDir)
                        .SeedAddTestData(
                            args.Where(x => x.StartsWith("seed")).ToList(),
                            args.Any(x => x == "append-cat-name"));

            }
            else
            {
                if (SunEngineDllRunServer(args))
                    RunServer(args);
                else
                    InfoPrinter.PrintVoidStartInfo();
            }
           
        }

        public static void RunServer(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            IHostingEnvironment env = (IHostingEnvironment) webHost.Services.GetService(typeof(IHostingEnvironment));
            IConfiguration conf = (IConfiguration) webHost.Services.GetService(typeof(IConfiguration));

            SetExceptionsMode(env, conf);

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    string dbSettingFile = Path.GetFullPath(Path.Combine(configDir, "DataBaseConnection.json"));
                    string mainSettingsFile = Path.GetFullPath(Path.Combine(configDir, "SunEngine.json"));
                    string logSettingsFile = Path.GetFullPath(Path.Combine(configDir, "LogConfig.json"));

                    config.AddJsonFile(logSettingsFile, false, false);
                    config.AddJsonFile(dbSettingFile, false, false);
                    config.AddJsonFile(mainSettingsFile, false, false);
                    config.AddCommandLine(args);
                });

        static bool SunEngineDllRunServer(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            IHostingEnvironment env = (IHostingEnvironment) webHost.Services.GetService(typeof(IHostingEnvironment));
            IConfiguration conf = (IConfiguration) webHost.Services.GetService(typeof(IConfiguration));

            if (bool.TryParse(conf["Dev:SunEngineDllRunServer"], out bool sunEngineDllRunServer))
            {
                if (sunEngineDllRunServer)
                    return true;
            }
            else if (env.IsDevelopment())
            {
                return true;
            }

            return false;
        }

        static void SetExceptionsMode(IHostingEnvironment env, IConfiguration conf)
        {
            void ShowExceptions()
            {
                Console.WriteLine("ShowExceptions mode");
                SunJsonContractResolver.ShowExceptions = true;
            }

            if (bool.TryParse(conf["Dev:ShowExceptions"], out bool showExceptions))
            {
                if (showExceptions)
                    ShowExceptions();
            }
            else if (env.IsDevelopment())
            {
                ShowExceptions();
            }
        }

    }
}