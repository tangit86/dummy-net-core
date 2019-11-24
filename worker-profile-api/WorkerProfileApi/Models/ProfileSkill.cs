namespace WorkerProfileApi.Models
{
    public class ProfileSkill
    {
        public int ProfileID { get; set; }
        public Profile Profile { get; set; }
        public int SkillID { get; set; }
        public Skill Skill { get; set; }
    }
}