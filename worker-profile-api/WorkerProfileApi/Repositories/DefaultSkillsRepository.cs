using System.Collections.Generic;
using System.Linq;
using WorkerProfileApi.Models;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Repositories
{

    public class DefaultSkillsRepository : ISkillsRepository
    {
        private readonly MyDbContextFactory _ctxFactory;
        public DefaultSkillsRepository(MyDbContextFactory contextFactory)
        {
            _ctxFactory = contextFactory;

        }
        public IEnumerable<Skill> Search(string q)
        {
            using(var context = _ctxFactory.Get())
            {
                var res = context.Skills.AsQueryable();

                if (!string.IsNullOrEmpty(q))
                {
                    res = res.Where(g => g.Name.Contains(q));
                }

                return res.ToList();
            }
        }
    }
}