using ClientNotebook.App_Data;
using ClientNotebook.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
        await CreateHostBuilder(args).Build()
            .MigrateDbContext<DbNotebookContext>()      //Статический класс для синхронизации созданных миграций
            .RunAsync();
                return 0;
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, configApp) => { configApp.AddCommandLine(args); })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }

}
