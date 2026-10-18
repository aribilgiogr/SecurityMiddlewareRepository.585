using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SecurityMiddlewareRepository._585.Abstractions;
using SecurityMiddlewareRepository._585.Configurations;
using SecurityMiddlewareRepository._585.Middlewares;

namespace SecurityMiddlewareRepository._585.Extensions
{
    public static class SecurityMiddlewareExtensitons
    {
        // Genel güvenlik doğrulama için gerekli servisleri ekler ve yapılandırma seçeneklerini ayarlar. Bu yapı olmadan arabirim uygulaması (middleware) çalışmaz.
        public static IServiceCollection AddGenericSecurity<TContext, TValidator>(this IServiceCollection services, Action<SecurityOptions>? configureOptions = null) where TContext : class, new() where TValidator : class, ISecurityValidator<TContext>
        {
            if (configureOptions is not null)
            {
                services.Configure(configureOptions);
            }
            else
            {
                services.AddOptions<SecurityOptions>();
            }

            services.TryAddScoped<ISecurityValidator<TContext>, TValidator>();

            return services;
        }

        // Önceden eklenmiş ayarları kullanarak genel güvenlik doğrulama arabirim uygulamasını (middleware) ekler.
        public static IApplicationBuilder UseGenericSecurity<TContext>(this IApplicationBuilder app) where TContext : class, new()
        {
            return app.UseMiddleware<GenericSecurityMiddleware<TContext>>();
        }
    }
}
