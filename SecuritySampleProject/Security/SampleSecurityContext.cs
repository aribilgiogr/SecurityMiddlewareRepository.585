namespace SecuritySampleProject.Security
{
    // TContext için hazırlanmış somut örnek. İstek sırasında taşınacak veya doğrulanacak güvenlik verilerini tutar.
    public sealed class SampleSecurityContext
    {
        public string? ClientIp { get; set; }
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
    }
}
