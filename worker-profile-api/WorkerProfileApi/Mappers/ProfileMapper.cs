using System;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.Geometries;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Mappers
{
    public class ProfileMapper : IProfileMapper
    {
        private readonly ISkillMapper _skillMapper;
        private readonly IGeometryMapper _geometryMapper;

        public ProfileMapper(ISkillMapper skillMapper, IGeometryMapper geometryMapper)
        {
            _skillMapper = skillMapper;
            _geometryMapper = geometryMapper;
        }
        public ProfileResponseDto map(Profile profile)
        {
            return new ProfileResponseDto()
            {
                ID = profile.ID,
                    Name = profile.Name,
                    Uid = profile.UID,
                    Skills = profile.ProfileSkills != null ? profile.ProfileSkills.Select(global => _skillMapper.map(global.Skill)).ToList() : new List<SkillDto>(),
                    Location = _geometryMapper.map(profile.Address, profile.Point)
            };
        }

        public Profile map(ProfileInputDto profile)
        {
            return new Profile()
            {
                Name = profile.Name,
                    Address = profile.Location != null ? profile.Location.Address : null,
                    Point = profile.Location != null ? _geometryMapper.map(profile.Location) : null,
                    ProfileSkills = profile.Skills != null ? profile.Skills.Select(g => _skillMapper.mapToSkillProfile(g)).ToList() : null
            };
        }
    }

    public interface IProfileMapper
    {
        ProfileResponseDto map(Profile profile);
        Profile map(ProfileInputDto profile);
    }
}