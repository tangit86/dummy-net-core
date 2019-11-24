using System.Collections.Generic;
using System.Linq;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Mappers;
using WorkerProfileApi.Repositories;

namespace WorkerProfileApi.Services
{
    public class SkillsService : ISkillsService
    {
        private readonly ISkillMapper _mapper;
        private readonly ISkillsRepository _skillsRepository;
        public SkillsService(ISkillsRepository skillsRepository, ISkillMapper mapper)
        {
            _mapper = mapper;
            _skillsRepository = skillsRepository;
        }
        public IEnumerable<SkillDto> GetSkills(string q)
        {
            return _skillsRepository.Search(q).Select(g => _mapper.map(g)).ToList();
        }
    }
}