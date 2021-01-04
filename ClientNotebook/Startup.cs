using ClientNotebook.App_Data;
using ClientNotebook.Entities;
using ClientNotebook.Interface;
using ClientNotebook.Repository;
using ClientNotebook.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //настройка соединения с бд
            services.AddDbContext<DbNotebookContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));       //Строка соединения находится в .json

            services.AddTransient<INotebookServices, NotebookServices>();   //Связываем интерфейсы и репозитории через DI

            services.AddTransient<IGenericRepository<Note>, GenericRepository<Note>>(); //!!!

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Notebook}/{action=Index}/{id?}");
            });
        }
    }
}
