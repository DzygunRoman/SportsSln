using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        private IConfiguration Configuration { get; set; }//Предоставляет доступ в систему конфигурации ASP.NET Core
        public void ConfigureServices(IServiceCollection services)//Применяется для настройки служб
        {
            services.AddControllersWithViews();//Настраивает совместно используемые объекты, требующиеся в приложениях, которые эксплуатируют MVC Framework механизм визуализации Razor
            services.AddDbContext<StoreDbContext>(opts => { opts.UseSqlServer(Configuration["ConnectionStrings:SportsStoreConnection"]); });//Регистрирует класс контекста базы данных и конфигурирует связь с базой данных
            services.AddScoped<IStoreRepository, EFStoreRepository>();//Создает службу, в которой каждый HTTP-запрос получает собственный объект хранилища
            services.AddRazorPages();//Включение инфраструктуры Razor Pages
            services.AddDistributedMemoryCache();//Настраивает хранилище данных в памяти
            services.AddSession();//Регистрирует службы, используемые для доступа к данным сеанса
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();//Добавляет простое сообщение в HTTP-ответы, которые иначе не имели бы тела(например 404-NOT Found)
            app.UseStaticFiles();//Включает поддержку для обслуживания статического содержимого из папки wwwroot
            app.UseSession();//позволяет системе сеансов автоматически ассоциировать запросы с сеансами, когда они поступают от клиента
            app.UseRouting();//Добавляют средства маршрутизации
            app.UseEndpoints(endpoints =>// для конечных точек
            {
                endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();//регистрирует Razor Pages в качестве конечной точки
            });
            //SeedData.EnsurePopulated(app);
        }
    }
}
