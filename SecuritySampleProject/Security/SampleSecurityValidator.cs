using SecurityMiddlewareRepository._585.Abstractions;

namespace SecuritySampleProject.Security
{
    public sealed class SampleSecurityValidator : ISecurityValidator<SampleSecurityContext>
    {
        public ValueTask<bool> ValidateAsync(HttpContext httpContext, SampleSecurityContext securityContext, CancellationToken cancellationToken = default)
        {
            securityContext.ClientIp = httpContext.Connection.RemoteIpAddress?.ToString();

            // Örnek kural: X-Security-Token başlığı değerini 'secret-123' olarak doğrula
            if (httpContext.Request.Headers.TryGetValue("X-Security-Token", out var tokenValue))
            {
                bool isValid = tokenValue == "secret-123";
                return ValueTask.FromResult(isValid);
            }

            return ValueTask.FromResult(false);
        }
    }
}
