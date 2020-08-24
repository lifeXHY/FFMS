using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FFMS.Application.AutoRegisterService
{
    public static class AddServiceExtension
    {
        /// <summary>
        /// 注入数据
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            #region 依赖注入
            //services.AddScoped<IUserService, UserService>();
            var baseType = typeof(IDenpendency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            var implementTypes = types.Where(x => x.IsClass).ToArray();
            var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                    //每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
                    services.AddScoped(interfaceType, implementType);
            }

            #endregion

            return services;
        }
    }
}
