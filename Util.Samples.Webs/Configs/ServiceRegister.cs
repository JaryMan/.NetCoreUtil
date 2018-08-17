using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Util.Datas.Dapper;
using Util.Dependency;
using Util.Files;
using Util.Files.Paths;
using Util.Randoms;
using Util.Samples.Services.Datas.DapperData;
using Util.Samples.Services.Datas.EFData;
using Util.Samples.Services.Models;
using Util.Samples.Services.Services;
using Util.Security.Identity.Describers;
using Util.Sessions;

namespace Util.Samples.Webs.Configs {
    /// <summary>
    /// 服务注册
    /// </summary>
    public class ServiceRegister : IDependencyRegistrar {
        /// <summary>
        /// 注册依赖
        /// </summary>
        public void Regist( IServiceCollection services ) {
            services.AddIdentity<IdentityUser, IdentityRole>();
            //.AddRoleStore<RoleRepository>();
            services.AddScoped<IdentityErrorDescriber, IdentityErrorChineseDescriber>();
            //services.AddScoped<IFileStore, DefaultFileStore>();
            //services.AddScoped<IPathGenerator, DefaultPathGenerator>();
            //services.AddSingleton<IBasePath>(new DefaultBasePath( "/upload" ));
            services.AddScoped<IRandomGenerator, GuidRandomGenerator>();

            #region 服务与数据层注入
            services.AddScoped<UserService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserData>();


            services.AddScoped<UserService2>();
            //services.AddScoped<UserRepository>();
            services.AddScoped<UserData2>();
            #endregion
        }
    }
}
