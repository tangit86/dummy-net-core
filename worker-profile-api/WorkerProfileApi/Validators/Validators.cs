using WorkerProfileApi.Dto;
using WorkerProfileApi.Exceptions;

namespace WorkerProfileApi.Validators
{
    public static class Validators
    {

        public static readonly int MAX_RADIUS = 500;
        public static void Validate(this ProfileInputDto profileInputDto)
        {
            if (string.IsNullOrWhiteSpace(profileInputDto.Name))
            {
                throw new InvalidInputException("Empty 'Profile.Name' not allowed");
            }

            if (profileInputDto.Location == null)
            {
                throw new InvalidInputException("Profile without location is not allowed");
            }
            profileInputDto.Location.Validate();
        }

        public static void Validate(this LocationDto locationDto)
        {
            if (string.IsNullOrWhiteSpace(locationDto.Address))
            {
                throw new InvalidInputException("Empty 'Address' not allowed");
            }

            if (!(locationDto.Longitude.IsLongitude() && locationDto.Latitude.IsLatitude()))
            {
                throw new InvalidInputException("Not correct Coordinates");
            }
        }

        public static void Validate(this LocationRadius locationRadius)
        {
            if (!(locationRadius.Longitude.IsLongitude() && locationRadius.Latitude.IsLatitude()))
            {
                throw new InvalidInputException("Not correct Coordinates");
            }

            if (locationRadius.Radius < 0 || locationRadius.Radius > MAX_RADIUS)
            {
                throw new InvalidInputException($"Maximum supported radius is {MAX_RADIUS}");
            }
        }

        public static bool IsLatitude(this double num)
        {
            return num >= -90 && num <= 90;
        }

        public static bool IsLongitude(this double num)
        {
            return num >= -180 && num <= 180;
        }
    }
}