using System.Collections.Generic;

namespace WorkerProfileApi.Dto
{
    public class ProfileResponseDto
    {
        public int ID { get; set; }

        public string Uid { get; set; }
        public string Name { get; set; }

        public List<SkillDto> Skills { get; set; }

        public LocationDto Location { get; set; }
    }
}