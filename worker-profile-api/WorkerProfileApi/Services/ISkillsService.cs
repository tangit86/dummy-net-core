using System.Collections.Generic;
using WorkerProfileApi.Dto;

namespace WorkerProfileApi.Services
{
    public interface ISkillsService
    {
        IEnumerable<SkillDto> GetSkills(string q);

    }
}