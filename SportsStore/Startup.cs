using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SportsStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)//Применяется для настройки служб
        {
            services.AddControllersWithViews();//Настраивает совместно используемые объекты, требующиеся в приложениях, которые эксплуатируют MVC Framework механизм визуализации Razor
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();//Добавляет простое сообщение в HTTP-ответы, которые иначе не имели бы тела(например 404-NOT Found)
            app.UseStaticFiles();//Включает поддержку для обслуживания статического содержимого из папки wwwroot
            app.UseRouting();//Добавляют средства маршрутизации
            app.UseEndpoints(endpoints =>// для конечных точек
            {
                endpoints.MapDefaultControllerRoute();//Региcтрация инфраструктуры MVC Framework как источника конечных точек
            });
        }
    }
}
