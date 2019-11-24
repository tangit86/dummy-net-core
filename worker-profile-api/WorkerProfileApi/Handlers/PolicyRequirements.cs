using System;
using Microsoft.AspNetCore.Authorization;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Handlers
{
    public class SameUserRequirement : IAuthorizationRequirement { }

    public class OwnerOrAdminRequirement : IAuthorizationRequirement { }
}