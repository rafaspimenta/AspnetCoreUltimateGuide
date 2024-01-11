using Microsoft.AspNetCore.Mvc;

namespace _1.ControllersExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/something")]
        public IActionResult Books()
        {
            return Content("<h1>redirected<h1>", "text/html");
        }
    }
}
