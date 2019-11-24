using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ISkillsService _skillsService;
        public SkillsController(ILogger<SkillsController> logger, ISkillsService skillsService)
        {
            _logger = logger;
            _skillsService = skillsService;
        }

        [HttpGet]

        [Authorize]
        public IEnumerable<SkillDto> GetSkills([FromQuery(Name = "q")] string q)
        {
            return _skillsService.GetSkills(q);
        }
    }
}