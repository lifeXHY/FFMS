using System;
using AutoMapper;
using FFMS.Application;
using FFMS.Application.AutoRegisterService;
using FFMS.EntityFrameWorkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Wei.Repository;

namespace FFMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public const string CookieScheme = "Cookies";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation()//�������ʱ����view��ͼ
                    .AddNewtonsoftJson(options =>
                    {
                        //�޸��������Ƶ����л���ʽ������ĸСд
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                        //�޸�ʱ������л���ʽ
                        options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd" });
                    })
                    ;
            services.AddMvc();
            //�������ݿ�����
            //services.AddDbContextPool<FFMSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            //  ���õ�������װ�Ĳִ�������EF CORE�����
            services.AddRepository<FFMSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddDataService();
            //services.AddTransient(typeof(IUserService), typeof(UserService));
            //���� AutoMapper������
            services.AddAutoMapper(typeof(AutomapperConfig));
            services.AddAuthentication(CookieScheme)
            .AddCookie(CookieScheme, option =>
            {
                // ��¼·�������ǵ��û���ͼ������Դ��δ���������֤ʱ�����򽫻Ὣ�����ض���������·����
                option.LoginPath = new PathString("/Account");
                // ��ֹ����·�������û���ͼ������Դʱ����δͨ������Դ���κ���Ȩ���ԣ����󽫱��ض���������·��
                //option.AccessDeniedPath = new PathString("/account/denied");
            });

            //����Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
