using System;
using System.Collections.Generic;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Repositories
{
    public interface IProfilesRepository
    {
        Profile GetProfile(int pid);
        Profile Create(Profile profile);
        Profile Update(int pid, Profile profile);
        Paginated<Profile> Search(string uid, IEnumerable<int> skills, LocationRadius locationRadius, int page, int pageSize);
    }
}