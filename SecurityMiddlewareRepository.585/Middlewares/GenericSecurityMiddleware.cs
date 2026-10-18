using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SecurityMiddlewareRepository._585.Abstractions;
using SecurityMiddlewareRepository._585.Configurations;

namespace SecurityMiddlewareRepository._585.Middlewares
{
    public sealed class GenericSecurityMiddleware<TContext>(RequestDelegate next, IOptionsMonitor<SecurityOptions> optionsMonitor) where TContext : class, new()
    {
        public async Task InvokeAsync(HttpContext context, ISecurityValidator<TContext> validator)
        {
            var options = optionsMonitor.CurrentValue;

            // Güvenlik doğrulamasını atlamak için hariç tutulan yolları kontrol et. (Örn. /health, /metrics)
            if (options.ExcludedPaths.Any(p => context.Request.Path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase)))
            {
                await next(context);
                return;
            }

            // Gerekli başlık doğrulamasını yap, kontrol edilen başlık varsayılan olarak "X-Security-Token" olarak ayarlanmıştır.
            if (options.EnableHeaderValidation && !context.Request.Headers.ContainsKey(options.RequiredHeaderName))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }


            // Güvenlik doğrulamasını gerçekleştir, TContext türünden bir güvenlik bağlamı oluştur ve doğrulayıcıyı çağır.
            var securityContext = new TContext();


            var isValid = await validator.ValidateAsync(context, securityContext, context.RequestAborted);

            // Eğer doğrulama başarısız olursa, 403 Forbidden yanıtı döndür.
            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            await next(context);
        }
    }
}
