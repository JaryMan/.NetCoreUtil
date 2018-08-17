using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Util.Datas.EFCore.Options;
using Util.Events.Default;
using Util.Logs.Extensions;
using Util.Samples.Services.Datas.Context;
using Util.Webs.Extensions;

namespace Util.Samples.Webs
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化启动配置
        /// </summary>
        /// <param name="configuration">配置</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置服务
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //添加Mvc服务
            services.AddMvc(options => {
                //options.Filters.Add( new AutoValidateAntiforgeryTokenAttribute() );
            }
            ).AddControllersAsServices();

            //添加NLog日志操作
            services.AddNLog();

            //添加事件总线服务
            services.AddEventBus();

            //////注册XSRF令牌服务
            //services.AddXsrfToken();

            #region 数据库注入            
            //DB1注入
            services.AddScoped(x => new EFContext(Configuration.GetConnectionString("DefaultConnection")));//注入EF上下文
            services.AddScoped<DapperContext>();//注入Dapper上下文

            //DB2注入
            services.AddScoped(x => new EFContext2(Configuration.GetConnectionString("DefaultConnection2")));//注入EF上下文
            services.AddScoped<DapperContext2>();//注入Dapper上下文
            #endregion

            //添加Swagger
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Info { Title = "Util Web Api Demo", Version = "v1" });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Util.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Util.Webs.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Util.Samples.Webs.xml"));
            });

            //添加Util基础设施服务
            return services.AddUtil();
        }

        /// <summary>
        /// 配置请求管道
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                DevelopmentConfig(app);
                return;
            }
            ProductionConfig(app);
        }

        /// <summary>
        /// 开发环境配置
        /// </summary>
        private void DevelopmentConfig(IApplicationBuilder app)
        {
            //app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseSwaggerX();
            CommonConfig(app);
        }

        /// <summary>
        /// 公共配置
        /// </summary>
        private void CommonConfig(IApplicationBuilder app)
        {
            app.UseErrorLog();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseXsrfToken();
            ConfigRoute(app);
        }

        /// <summary>
        /// 路由配置,支持区域
        /// </summary>
        private void ConfigRoute(IApplicationBuilder app)
        {
            app.UseMvc(routes => {
                routes.MapRoute("api", "api/{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }

        /// <summary>
        /// 生产环境配置
        /// </summary>
        private void ProductionConfig(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error");
            CommonConfig(app);
        }
    }
}
