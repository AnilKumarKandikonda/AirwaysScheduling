using System.ComponentModel;

namespace FlightScheduling
{
    public class Flight
    {
        public int FlightId { get; set; }
        public required string Departure { get; set; }
        public required string Arrival { get; set; }
        public int Capacity { get; set; } = 20;
        public required int Day { get; set; }
        public List<string> Orders { get; set; } = [];
        public Flight() { }

        public Flight(int flightId, string departure, string arrival, int day) { 
            FlightId = flightId;
            Departure = departure;
            Arrival = arrival;
            Day = day;
        }

        public bool AddOrder(string OrderId) {
            if (Orders.Count < Capacity) { 
                Orders.Add(OrderId);
                return true;
            }
            return false;
        }

        public bool IsFlightFull() { 
            return Orders.Count >= Capacity;
        }

        public override string ToString() {
            return $"Flight {FlightId}: {Locations.GetLocationName(Departure)}({Departure}) to {Locations.GetLocationName(Arrival)}({Arrival})";
        }
    }

    public class Order { 
        public required string OrderId { get; set; }
        public required Dictionary<string,string> Destination { get; set; }
    }
}
