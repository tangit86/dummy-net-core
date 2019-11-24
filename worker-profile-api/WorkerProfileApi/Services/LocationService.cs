using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WorkerProfileApi.Dto;

namespace WorkerProfileApi.Services
{
    public class LocationService : ILocationService
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        private readonly string _baseUrl = "https://maps.googleapis.com/maps/api/geocode/json?";
        public LocationService(string apiKey)
        {
            _apiKey = apiKey;
            _client = new HttpClient();
        }
        public async Task<IEnumerable<LocationDto>> Search(string q)
        {
            var httpResponse = await _client.GetAsync($"{_baseUrl}address={q}&key={_apiKey}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot fetch locations");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            JObject result = JObject.Parse(content);

            try
            {
                JArray locations = (JArray) result["results"];
                return locations.Select(loc => new LocationDto()
                {
                    Address = (string) loc["formatted_address"],
                        Latitude = Convert.ToDouble(loc["geometry"]["location"]["lat"]),
                        Longitude = Convert.ToDouble(loc["geometry"]["location"]["lng"])
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error deserializing geocoding API response", ex);
            }

        }

    }
}