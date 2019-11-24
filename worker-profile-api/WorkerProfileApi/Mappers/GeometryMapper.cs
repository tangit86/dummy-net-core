using NetTopologySuite.Geometries;
using WorkerProfileApi.Dto;

namespace WorkerProfileApi.Mappers
{
    public interface IGeometryMapper
    {
        Point map(LocationDto locationDto);

        LocationDto map(string address, Point point);

        Point map(LocationRadius locationRadius);

        Point map(double lat, double lng);
    }

    public class GeometryMapper : IGeometryMapper
    {
        private readonly GeometryFactory _geometryFactory;
        public GeometryMapper(GeometryFactory geometryFactory)
        {
            _geometryFactory = geometryFactory;
        }
        public Point map(LocationDto locationDto)
        {
            return map(locationDto.Latitude, locationDto.Longitude);
        }

        public LocationDto map(string address, Point point)
        {
            return new LocationDto() { Address = address, Latitude = point.Y, Longitude = point.X };
        }

        public Point map(LocationRadius locationRadius)
        {
            return map(locationRadius.Latitude, locationRadius.Longitude);
        }

        public Point map(double lat, double lng)
        {
            return _geometryFactory.CreatePoint(new Coordinate(lng, lat));
        }
    }
}