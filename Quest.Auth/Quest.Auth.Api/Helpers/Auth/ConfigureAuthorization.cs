using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Quest.Auth.Api.Helpers.Auth
{
    public static class ConfigureAuthorization
    {
        public static void Init(IServiceCollection services, string domain, string audience)
        {
            AddAuthentication(services, domain, audience);
            AddAuthorization(services, domain);

            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, AuthScopeHandler>();
        }
        private static void AddAuthentication(IServiceCollection services, string domain, string audience)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = domain;
                        options.Audience = audience;

                    });
        }
        private static void AddAuthorization(IServiceCollection services, string domain)
        {
            //get auth scope list
            List<Type> scopeTypes = typeof(AuthorizationScope).Assembly.GetTypes()
                .Where(t => t.IsClass && t.IsSealed && t.IsAbstract && t.DeclaringType != null
                  && t.DeclaringType.Name == nameof(AuthorizationScope)).ToList();

            services.AddAuthorization(options =>
            {
                foreach (Type st in scopeTypes)
                {
                    List<FieldInfo> scopeProps = GetConstants(st);

                    scopeProps.ForEach(t =>
                    {
                        string scopeValue = t.GetRawConstantValue().ToString();

                        options.AddPolicy(scopeValue, policy => policy.Requirements.Add(new AuthScopeRequirement(scopeValue, domain)));
                    });
                }
            });
        }
        private static List<FieldInfo> GetConstants(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }

    }
}
