using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Exceptions;
using WorkerProfileApi.Mappers;
using WorkerProfileApi.Models;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Repositories
{
    public class DefaultProfilesRepository : IProfilesRepository
    {

        private readonly MyDbContextFactory _ctxFactory;

        private readonly IGeometryMapper _geometryMapper;
        public DefaultProfilesRepository(MyDbContextFactory contextFactory, IGeometryMapper geometryMapper)
        {
            _ctxFactory = contextFactory;
            _geometryMapper = geometryMapper;
        }

        public Profile Create(Profile profile)
        {
            checkExistence(0, profile.Name);

            using(var context = _ctxFactory.Get())
            {
                context.Attach(profile);
                context.ProfileSkills.Include(x => x.Skill);
                context.SaveChanges();
                return GetProfile(profile.ID);
            }
        }

        public Profile GetProfile(int pid)
        {
            using(var context = _ctxFactory.Get())
            {
                return context.Profiles.Include(x => x.ProfileSkills).ThenInclude(s => s.Skill).FirstOrDefault(x => x.ID.Equals(pid));
            }
        }

        private Profile GetProfileByIdAndName(int pid, string name)
        {
            using(var context = _ctxFactory.Get())
            {
                return context.Profiles.FirstOrDefault(x => x.Name.Equals(name) && !x.ID.Equals(pid));
            }
        }

        public Paginated<Profile> Search(string uid, IEnumerable<int> skills, LocationRadius locationRadius, int page, int pageSize)
        {
            using(var context = _ctxFactory.Get())
            {
                var res = context.Profiles.Include(p => p.ProfileSkills).ThenInclude(ps => ps.Skill).AsQueryable<Profile>();

                if (skills != null)
                {
                    res = res.Where(p => p.ProfileSkills.Where(s => skills.Contains(s.SkillID)).Count().Equals(skills.Count()));
                }

                if (!string.IsNullOrEmpty(uid))
                {
                    res = res.Where(p => p.UID.Equals(uid));
                }

                if (locationRadius != null)
                {
                    res = res.Where(p => p.Point.Distance(_geometryMapper.map(locationRadius)) <= locationRadius.Radius);
                }

                return new Paginated<Profile>(page, pageSize, res);
            }
        }

        public Profile Update(int pid, Profile profile)
        {
            using(var context = _ctxFactory.Get())
            {
                var existing = context.Profiles.Include(x => x.ProfileSkills).ThenInclude(x => x.Skill).FirstOrDefault(x => x.ID.Equals(pid));

                if (existing == null)
                {
                    throw new ProfileNotFoundException("Didn't find profile");
                }

                checkExistence(pid, profile.Name);
                if (profile.Name != existing.Name)
                {
                    existing.Name = profile.Name;
                }

                if (profile.Address != existing.Address)
                {
                    existing.Address = profile.Address;
                    existing.Point = profile.Point;
                }

                existing.ProfileSkills
                    .Except(profile.ProfileSkills)
                    .ToList()
                    .ForEach(x => existing.ProfileSkills.Remove(x));

                profile.ProfileSkills
                    .Except(existing.ProfileSkills)
                    .ToList()
                    .ForEach(x => existing.ProfileSkills.Add(x));

                context.Profiles.Update(existing);
                context.SaveChanges();
            }

            return GetProfile(pid);
        }

        private void checkExistence(int pid, string name)
        {
            if (this.GetProfileByIdAndName(pid, name) != null)
            {
                throw new ProfileExistsException("There is already an other profile with the same Name");
            }

        }
    }
}