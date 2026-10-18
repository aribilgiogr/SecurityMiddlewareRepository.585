using Microsoft.AspNetCore.Http;

namespace SecurityMiddlewareRepository._585.Abstractions
{
    public interface ISecurityValidator<TContext> where TContext : class
    {
        ValueTask<bool> ValidateAsync(HttpContext httpContext, TContext securityContext, CancellationToken cancellationToken = default);
    }
}
