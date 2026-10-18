using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityMiddlewareRepository._585.Configurations
{
    public sealed class SecurityOptions
    {
        public bool EnableHeaderValidation { get; set; } = true;
        public string RequiredHeaderName { get; set; } = "X-Security-Token";
        public IEnumerable<string> ExcludedPaths { get; set; } = [];
    }
}
