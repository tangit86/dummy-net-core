using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WorkerProfileApi.Mappers;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Handlers
{
    public class ProfileAccessAuthorizationHandler : AuthorizationHandler<OwnerOrAdminRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerOrAdminRequirement requirement, string profileOwnerUid)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.User.GetSub() == profileOwnerUid)
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