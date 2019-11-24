using WorkerProfileApi.Dto;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Mappers
{
    public class SkillMapper : ISkillMapper
    {
        public SkillDto map(Skill skill)
        {
            return new SkillDto() { ID = skill.ID, Name = skill.Name };
        }

        public SkillDto map(ProfileSkill profileSkill)
        {
            return map(profileSkill.Skill);
        }

        public ProfileSkill mapToSkillProfile(SkillDto skill)
        {
            if (skill.ID > 0)
            {
                return new ProfileSkill { SkillID = skill.ID };
            }

            return new ProfileSkill { Skill = map(skill) };

        }

        public Skill map(SkillDto dto)
        {
            return new Skill() { ID = dto.ID, Name = dto.Name };
        }
    }

    public interface ISkillMapper
    {
        SkillDto map(Skill skill);

        Skill map(SkillDto dto);

        SkillDto map(ProfileSkill skill);

        ProfileSkill mapToSkillProfile(SkillDto skill);
    }
}