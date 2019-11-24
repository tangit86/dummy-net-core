using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Exceptions;
using WorkerProfileApi.Handlers;
using WorkerProfileApi.Mappers;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IAuthorizationService _authService;

        private readonly ILogger _logger;
        private readonly IProfileService _profileService;
        public ProfilesController(ILogger<ProfilesController> logger, IProfileService profileService, IAuthorizationService authService)
        {
            _logger = logger;
            _profileService = profileService;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("{pid}")]
        public async Task<ProfileResponseDto> ReadProfile(int pid)
        {
            var profile = _profileService.GetProfile(pid);
            var t = await _authService.AuthorizeAsync(this.User, profile.Uid, new OwnerOrAdminRequirement());
            if (!t.Succeeded)
            {
                throw new UnauthorizedResourceAccessException("Not authorized to access this Profile");
            }

            return profile;
        }

        [Authorize]
        [HttpPut("{pid}")]
        public async Task<ProfileResponseDto> UpdateProfile([FromRoute(Name = "pid")] int pid, [FromBody] ProfileInputDto profile)
        {

            var p = _profileService.GetProfile(pid);

            var t = await _authService.AuthorizeAsync(this.User, p.Uid, new OwnerOrAdminRequirement());

            if (!t.Succeeded) { throw new UnauthorizedResourceAccessException("Not authorized to update this Profile"); }

            return _profileService.UpdateProfile(pid, profile);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<ProfileResponseDto> CreateProfile([FromBody] ProfileInputDto profile)
        {
            var uid = this.User.GetSub();
            return _profileService.CreateProfile(uid, profile);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("search")] public ActionResult<Paginated<ProfileResponseDto>> SearchProfiles([FromQuery(Name = "skills")] List<int> skills, [FromQuery(Name = "lat")] double lat, [FromQuery(Name = "lng")] double lng, [FromQuery(Name = "radius")] int radius, [FromQuery(Name = "page")] int page, [FromQuery(Name = "pageSize")] int pageSize)
        {
            return _profileService.Search(null, skills, new LocationRadius() { Latitude = lat, Longitude = lng, Radius = radius }, page, pageSize);
        }
    }
}