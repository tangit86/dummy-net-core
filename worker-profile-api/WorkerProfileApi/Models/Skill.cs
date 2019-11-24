using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite;

namespace WorkerProfileApi.Models
{

    public class Skill
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ProfileSkill> ProfileSkills { get; set; }
    }
}