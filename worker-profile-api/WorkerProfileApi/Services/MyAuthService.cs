using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WorkerProfileApi.Services
{
    public class MyAuthService : IAuthorizationService
    {
        IAuthorizationService authBase;

        public MyAuthService(IAuthorizationPolicyProvider policyProvider, IAuthorizationHandlerProvider handlers, ILogger<DefaultAuthorizationService> logger, IAuthorizationHandlerContextFactory contextFactory, IAuthorizationEvaluator evaluator, IOptions<AuthorizationOptions> options)
        {
            this.authBase = new DefaultAuthorizationService(policyProvider, handlers, logger, contextFactory, evaluator, options);
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            return this.authBase.AuthorizeAsync(user, resource, requirements);
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            throw new System.NotImplementedException();
        }
    }
}