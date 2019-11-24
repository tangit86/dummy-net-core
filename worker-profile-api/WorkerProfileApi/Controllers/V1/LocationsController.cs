using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [Authorize]
        public async Task<IEnumerable<LocationDto>> Search(string q)
        {
            return await _locationService.Search(q);
        }
    }
}