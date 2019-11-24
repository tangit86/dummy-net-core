using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using NetTopologySuite.Geometries;

namespace WorkerProfileApi.Mappers
{
    public static class MyExtensions
    {

        public static string PointToCoordinateString(this Point point)
        {
            return point.X.ToString() + "," + point.Y.ToString();
        }

        public static string GetSub(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.First(x => x.Type.Equals("sub")).Value.ToString();
        }

        public static HashSet<string> GetPermissions(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(x => x.Type.Equals("permissions")).Select(x => x.Value.ToString()).ToHashSet();
        }

        public static HashSet<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(x => x.Type.Equals("http://my.roles/")).Select(x => x.Value.ToString()).ToHashSet();
        }
    }
}