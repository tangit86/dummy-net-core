using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WorkerProfileApi.Mappers;

namespace WorkerProfileApi.Handlers
{
    public class PersonalizedRouteAuthorizationHandler : AuthorizationHandler<SameUserRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, string uid)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.User.GetSub() == uid)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}