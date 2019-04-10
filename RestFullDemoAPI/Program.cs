using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace RestFullDemoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            var env = Environment.GetEnvironmentVariable("MATRIXONE_ENVIRONMENT");
            Console.WriteLine($"====MATRIXONE_ENVIRONMENT={env}==============");
            if (string.IsNullOrEmpty(env))
                env = "local";
            //NLogBuilder.ConfigureNLog($"nlog.{env}.config");
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Language/zh_CN.json", optional: true, reloadOnChange: true)
            .AddJsonFile("Language/en_US.json", optional: true, reloadOnChange: true)
            //.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddCommandLine(args)
            .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseConfiguration(config)
                .UseStartup<Startup>()
                //.UseNLog()
                .Build();
        }
    }
}
