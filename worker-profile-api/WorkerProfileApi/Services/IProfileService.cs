using System.Collections.Generic;
using WorkerProfileApi.Dto;

namespace WorkerProfileApi.Services
{
    public interface IProfileService
    {
        ProfileResponseDto GetProfile(int pid);

        ProfileResponseDto CreateProfile(string uid, ProfileInputDto profile);

        ProfileResponseDto UpdateProfile(int pid, ProfileInputDto profile);

        Paginated<ProfileResponseDto> Search(string uid, IEnumerable<int> skills, LocationRadius locationRadius, int page, int pageSize);
    }
}