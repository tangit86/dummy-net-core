using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace WorkerProfileApi.Models
{

    public class Profile
    {
        public int ID { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string UID { get; set; }
        public string Address { get; set; }

        // [Column(TypeName = "geometry")]
        // public NetTopologySuite.Geometries.Point Point { get; set; }

        public Point Point { get; set; }

        public ICollection<ProfileSkill> ProfileSkills { get; set; }
    }

}