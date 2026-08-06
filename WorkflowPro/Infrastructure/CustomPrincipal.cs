using System;
using System.Linq;
using System.Security.Principal;

namespace WorkflowPro.Infrastructure
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly string[] _roles;

        public CustomPrincipal(IIdentity identity, string[] roles)
        {
            Identity = identity;
            _roles = roles ?? new string[0];
        }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role)) return false;
            return _roles.Any(r => string.Equals(r, role, StringComparison.OrdinalIgnoreCase));
        }
    }
}
