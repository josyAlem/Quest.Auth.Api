using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quest.Auth.Api", Version = "v1" });
            });

            // 1. Add Authentication Services
            var domain = Configuration["Auth0:Domain"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];
               
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationScope.Products.Get, policy => policy.Requirements.Add(new AuthorizationScopeRequirement(AuthorizationScope.Products.Get, domain)));
            });
            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, AuthorizationScopeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration config)
        {
             string SwaggerGenerationConfiguration = "SwaggerGenerationSettings";
            string AppVersion = "AppVersion";
       string AppName = "AppName";
       string VirtualDirectory = "VirtualDirectory"; 
        IConfigurationSection swaggerGenerationConfig = config.GetSection(SwaggerGenerationConfiguration);
            if (swaggerGenerationConfig == null)
            {
                throw new ArgumentNullException(nameof(SwaggerGenerationConfiguration), $"{SwaggerGenerationConfiguration} does not exist in appsettings.json");
            }

            string appVersion = swaggerGenerationConfig[AppVersion];
            string virtualDirectory = swaggerGenerationConfig[VirtualDirectory];
            string appName = swaggerGenerationConfig[AppName];

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

            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
