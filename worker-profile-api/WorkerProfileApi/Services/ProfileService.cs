using System.Collections.Generic;
using System.Linq;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Exceptions;
using WorkerProfileApi.Mappers;
using WorkerProfileApi.Repositories;
using WorkerProfileApi.Validators;

namespace WorkerProfileApi.Services {
    public class ProfileService : IProfileService {

        private readonly IProfileMapper _profileMapper;
        private readonly IProfilesRepository _profilesRepository;
        public ProfileService (IProfilesRepository profilesRepository, IProfileMapper profileMapper) {
            _profileMapper = profileMapper;
            _profilesRepository = profilesRepository;
        }

        public ProfileResponseDto CreateProfile (string uid, ProfileInputDto profileInputDto) {
            profileInputDto.Validate ();
            var profile = _profileMapper.map (profileInputDto);
            profile.UID = uid;
            return _profileMapper.map (_profilesRepository.Create (profile));
        }

        public ProfileResponseDto UpdateProfile (int pid, ProfileInputDto profileInputDto) {
            profileInputDto.Validate ();

            var profile = _profileMapper.map (profileInputDto);

            return _profileMapper.map (_profilesRepository.Update (pid, profile));
        }

        public ProfileResponseDto GetProfile (int pid) {
            var profile = _profilesRepository.GetProfile (pid);
            if (profile == null) {
                throw new ProfileNotFoundException ("Didn't find profile");
            }
            return _profileMapper.map (profile);
        }

        public Paginated<ProfileResponseDto> Search (string uid, IEnumerable<int> skills, LocationRadius locationRadius, int page, int pageSize) {
            IEnumerable<int> skillsParam = null;
            string uidParam = null;
            LocationRadius locationRadiusParam = null;

            if (skills != null && skills.Count () > 0) {
                skillsParam = skills;
            }

            if (!string.IsNullOrEmpty (uid)) {
                uidParam = uid;
            }

            if (locationRadius != null && locationRadius.Radius > 0) {
                locationRadius.Validate ();
                locationRadiusParam = new LocationRadius () {
                    Radius = locationRadius.Radius * 1000,
                    Longitude = locationRadius.Longitude,
                    Latitude = locationRadius.Latitude
                };
            }

            return _profilesRepository.Search (uidParam, skillsParam, locationRadiusParam, page, pageSize).To<ProfileResponseDto> (_profileMapper.map);
        }

    }
}