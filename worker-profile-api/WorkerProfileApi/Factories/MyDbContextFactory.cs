using Microsoft.EntityFrameworkCore;
using WorkerProfileApi.Models;

namespace WorkerProfileApi.Services
{
    public class MyDbContextFactory
    {

        DbContextOptionsBuilder<WorkerProfileApiContext> builder = new DbContextOptionsBuilder<WorkerProfileApiContext>();

        public MyDbContextFactory(string connectionString)
        {
            builder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
        }
        public WorkerProfileApiContext Get()
        {
            return new WorkerProfileApiContext(builder.Options);
        }

    }
}