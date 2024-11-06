using FlightScheduling;
using System.Text.Json;

class Scheduling {
    public List<Flight> flights = [];
    public List<Order> orders = [];

    public void LoadFlightsData() {
        flights.AddRange([  
            new() { FlightId = 1,Departure = "YUL",Arrival = "YYZ", Day = 1},
            new() { FlightId = 2, Departure = "YUL", Arrival = "YYC", Day = 1 },
            new() { FlightId = 3, Departure = "YUL", Arrival = "YVR", Day = 1 },
            new() { FlightId = 4, Departure = "YUL", Arrival = "YYZ", Day = 2 },
            new() { FlightId = 5, Departure = "YUL", Arrival = "YYC", Day = 2 },
            new() { FlightId = 6, Departure = "YUL", Arrival = "YVR", Day = 2 }
        ]);
    }

    public void DisplayFlightSchedules() {
        var groupedData = flights.GroupBy(f => f.Day);
        foreach (var flightsGroup in groupedData)
        {
            int day = flightsGroup.Key;  
            Console.WriteLine($"Day {day}:");

            foreach (var flight in flightsGroup)
            {
                Console.WriteLine(flight);
            }
        }
    }

    private static List<Order> LoadOrdersFromJson(string filePath)
    {
        // Read the entire JSON file
        string json = File.ReadAllText(filePath);

        // Deserialize the JSON into a Dictionary<string, Order>
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
        if (jsonData == null) return [];
        List<Order> orders = [];
        foreach (var entry in jsonData)
        {
            var orderId = entry.Key;
            entry.Value.TryGetValue("destination", out var destinationValue);
            var destination = new Dictionary<string, string>
            {
                
                { "destination", destinationValue??"" }
            };

            orders.Add(new Order
            {
                OrderId = orderId,
                Destination = destination
            });
        }

        return orders;
    }

    public void LoadOrders() {
        string jsonFilePath = "E:\\AirTek\\orders.json";

        orders.AddRange(LoadOrdersFromJson(jsonFilePath));
    }

    public void PrintOrders() {
        foreach (var order in orders) {
            var flight = flights.OrderBy(d => d.Day).FirstOrDefault(f => order.Destination["destination"].ToString() == f.Arrival && !f.IsFlightFull());
            if (flight == null)
            {
                var pending = flights.FirstOrDefault(f => f.Arrival == order.Destination["destination"]);
                //if (pending == null) continue;
                Console.WriteLine($"order: order-{order.OrderId}, {order.Destination["destination"]}: not scheduled");
            }
            else {
                flight.Orders.Add(order.OrderId);
                Console.WriteLine($"order: {order.OrderId},flightNumber:{flight.FlightId}," +
                    $"departure:{Locations.GetLocationName(flight.Departure)},arrival:{Locations.GetLocationName(flight.Arrival)},Day:{flight.Day}");
            }
        }
    }
}


class Program
{
    public static void Main()
    {
        Scheduling scheduling = new();
        scheduling.LoadFlightsData();
        scheduling.DisplayFlightSchedules();
        scheduling.LoadOrders();
        scheduling.PrintOrders();
    }
}
