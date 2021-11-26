using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quest.Auth.Services;
using Quest.Auth.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using Studio.Auth.Auth0.Config;
using Quest.Auth.Api.Helpers.Auth;

namespace Quest.Auth.Api
{
    public class Startup
    {

        private readonly string VirtualDirectory = "VirtualDirectory";
        private readonly string appVersion;
        private readonly string virtualDirectory;
        private readonly string appName;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string SwaggerGenerationConfiguration = "SwaggerGenerationSettings";
            string AppVersion = "AppVersion";
            string AppName = "AppName";
            IConfigurationSection swaggerGenerationConfig = configuration.GetSection(SwaggerGenerationConfiguration);
            if (swaggerGenerationConfig == null)
            {
                throw new ArgumentNullException(nameof(SwaggerGenerationConfiguration), $"{SwaggerGenerationConfiguration} does not exist in appsettings.json");
            }

            appVersion = swaggerGenerationConfig[AppVersion];
            virtualDirectory = swaggerGenerationConfig[VirtualDirectory];
            appName = swaggerGenerationConfig[AppName];

            //var auth0Setting = auth0Config.Get<Auth0Settings>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
              .Enrich.WithProperty("Application", appName)
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .WriteTo.File(path: "C:\\Logs\\Quest.Auth.Api\\Quest.Auth.Api-.log",
              restrictedToMinimumLevel:Serilog.Events.LogEventLevel.Information,
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{RequestId}] {Message}{NewLine}{Exception}",
              null,fileSizeLimitBytes: 104857600,null,
              false,false,null,rollingInterval:RollingInterval.Day,
              rollOnFileSizeLimit:true,retainedFileCountLimit:60,null,null,null
              )
              .CreateLogger();
             

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = appVersion });
            });

            #region Authentication and Authorization
            //get auth scope list
            List<Type> scopeTypes = typeof(AuthorizationScope).Assembly.GetTypes()
                .Where(t => t.IsClass && t.IsSealed && t.IsAbstract && t.DeclaringType != null
                  && t.DeclaringType.Name == nameof(AuthorizationScope)).ToList(); 
           
            IConfiguration auth0Config = Configuration.GetSection("Auth0");
            
            Auth0Middleware.Init(services, auth0Config, scopeTypes);
            #endregion

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {

                o.RoutePrefix = "api-docs";

                //Allow to show addition attribute info on doc. Example: [MaxLength(50)]
                o.ShowCommonExtensions();

                string swaggerEndpoint = $"/{virtualDirectory}swagger/{appVersion}/swagger.json";
                o.SwaggerEndpoint(swaggerEndpoint, appName);
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
