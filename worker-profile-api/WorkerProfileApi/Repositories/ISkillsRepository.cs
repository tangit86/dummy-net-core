using System.Collections.Generic;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Repositories
{
    public interface ISkillsRepository
    {
        IEnumerable<Skill> Search(string q);
    }
}