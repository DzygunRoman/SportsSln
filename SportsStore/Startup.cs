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
        public void ConfigureServices(IServiceCollection services)//����������� ��� ��������� �����
        {
            services.AddControllersWithViews();//����������� ��������� ������������ �������, ����������� � �����������, ������� ������������� MVC Framework �������� ������������ Razor
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();//��������� ������� ��������� � HTTP-������, ������� ����� �� ����� �� ����(�������� 404-NOT Found)
            app.UseStaticFiles();//�������� ��������� ��� ������������ ������������ ����������� �� ����� wwwroot
            app.UseRouting();//��������� �������� �������������
            app.UseEndpoints(endpoints =>// ��� �������� �����
            {
                endpoints.MapDefaultControllerRoute();//����c������ �������������� MVC Framework ��� ��������� �������� �����
            });
        }
    }
}
