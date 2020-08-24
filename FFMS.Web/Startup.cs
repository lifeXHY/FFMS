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
                    .AddRazorRuntimeCompilation()//添加运行时编译view视图
                    .AddNewtonsoftJson(options =>
                    {
                        //修改属性名称的序列化方式，首字母小写
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                        //修改时间的序列化方式
                        options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd" });
                    })
                    ;
            services.AddMvc();
            //配置数据库连接
            //services.AddDbContextPool<FFMSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            //  引用第三方封装的仓储（基于EF CORE）框架
            services.AddRepository<FFMSDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddDataService();
            //services.AddTransient(typeof(IUserService), typeof(UserService));
            //加载 AutoMapper配置类
            services.AddAutoMapper(typeof(AutomapperConfig));
            services.AddAuthentication(CookieScheme)
            .AddCookie(CookieScheme, option =>
            {
                // 登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径。
                option.LoginPath = new PathString("/Account");
                // 禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径
                //option.AccessDeniedPath = new PathString("/account/denied");
            });

            //配置Session
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
