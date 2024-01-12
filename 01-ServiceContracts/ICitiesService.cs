
namespace _01_ServiceContracts;

public interface ICitiesService
{
    int ServiceInstanceId { get; }
    IList<string> GetCities();
}