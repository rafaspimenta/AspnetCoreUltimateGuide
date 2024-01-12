using _01_ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace _01_DIExample.Controllers
{
    public class HomeController(ICitiesService citiesService1, ICitiesService citiesService2, ICitiesService citiesService3, IServiceScopeFactory scopeFactory) : Controller
    {
        private readonly ICitiesService _citiesService1 = citiesService1;
        private readonly ICitiesService _citiesService2 = citiesService2;
        private readonly ICitiesService _citiesService3 = citiesService3;

        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

        [Route("/")]
        public IActionResult Index()
        {
            var cities = _citiesService1.GetCities();

            ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;

            using IServiceScope scope = _scopeFactory.CreateScope();
            var citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();

            ViewBag.InstanceId_CitiesService_InScope = citiesService.ServiceInstanceId;

            return View(cities);
        }
    }
}
