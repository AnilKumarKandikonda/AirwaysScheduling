using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduling
{
    public class Locations
    {
        private static readonly Dictionary<string, string> locations = new Dictionary<string, string>
        {
            { "YUL", "Montreal airport" },
            { "YYZ","Toronto"},
            { "YYC","Calgary"},
            { "YVR","Vancouver"}
        };
        public static string GetLocationName(string code) {
            return locations.TryGetValue(code, out var location) ? location : "Location not found";
        }
    }
}
