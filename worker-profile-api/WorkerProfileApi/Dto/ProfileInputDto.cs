using System.Collections.Generic;

namespace WorkerProfileApi.Dto
{
    public class ProfileInputDto
    {
        public string Name { get; set; }

        public List<SkillDto> Skills { get; set; }

        public LocationDto Location { get; set; }
    }
}