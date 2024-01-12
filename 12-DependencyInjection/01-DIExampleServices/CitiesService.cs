using _01_ServiceContracts;
using System.Runtime.CompilerServices;

namespace _01_DIExampleServices;

public class CitiesService : ICitiesService, IDisposable
{
    public int ServiceInstanceId { get; }
    private readonly IList<string> _cities;

    public CitiesService()
    {
        ServiceInstanceId = GetHashCode();

        _cities = new List<string>()
        {
            "London",
            "Paris",
            "New York",
            "Tokyo",
            "Rome"
        };
    }


    public IList<string> GetCities()
    {
        return _cities;
    }

    public void Dispose()
    {
        //
    }
}
