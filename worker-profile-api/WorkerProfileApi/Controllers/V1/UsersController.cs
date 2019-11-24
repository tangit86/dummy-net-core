using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Exceptions;
using WorkerProfileApi.Handlers;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        private readonly ILogger _logger;
        private readonly IProfileService _profileService;
        public UsersController(ILogger<UsersController> logger, IProfileService profileService, IAuthorizationService authService)
        {
            _logger = logger;
            _profileService = profileService;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("{uid}/profiles")]
        public async Task<Paginated<ProfileResponseDto>> ReadUserProfiles(string uid, int page, int pageSize)
        {
            var t = await _authService.AuthorizeAsync(this.User, uid, new SameUserRequirement());
            if (!t.Succeeded) { throw new UnauthorizedResourceAccessException("Not authorized to access this Profile"); }
            return _profileService.Search(uid, null, null, page, pageSize);
        }

    }
}