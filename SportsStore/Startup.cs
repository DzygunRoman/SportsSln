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
        private IConfiguration Configuration { get; set; }//������������� ������ � ������� ������������ ASP.NET Core
        public void ConfigureServices(IServiceCollection services)//����������� ��� ��������� �����
        {
            services.AddControllersWithViews();//����������� ��������� ������������ �������, ����������� � �����������, ������� ������������� MVC Framework �������� ������������ Razor
            services.AddDbContext<StoreDbContext>(opts => { opts.UseSqlServer(Configuration["ConnectionStrings:SportsStoreConnection"]); });//������������ ����� ��������� ���� ������ � ������������� ����� � ����� ������
            services.AddScoped<IStoreRepository, EFStoreRepository>();//������� ������, � ������� ������ HTTP-������ �������� ����������� ������ ���������
            services.AddRazorPages();//��������� �������������� Razor Pages
            services.AddDistributedMemoryCache();//����������� ��������� ������ � ������
            services.AddSession();//������������ ������, ������������ ��� ������� � ������ ������
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();//��������� ������� ��������� � HTTP-������, ������� ����� �� ����� �� ����(�������� 404-NOT Found)
            app.UseStaticFiles();//�������� ��������� ��� ������������ ������������ ����������� �� ����� wwwroot
            app.UseSession();//��������� ������� ������� ������������� ������������� ������� � ��������, ����� ��� ��������� �� �������
            app.UseRouting();//��������� �������� �������������
            app.UseEndpoints(endpoints =>// ��� �������� �����
            {
                endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();//������������ Razor Pages � �������� �������� �����
            });
            //SeedData.EnsurePopulated(app);
        }
    }
}
