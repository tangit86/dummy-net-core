using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerProfileApi.Dto;

namespace WorkerProfileApi.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> Search(string q);
    }
}