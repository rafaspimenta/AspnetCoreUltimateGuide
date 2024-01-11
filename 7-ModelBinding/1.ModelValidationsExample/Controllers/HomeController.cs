using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers
{
    public class HomeController: ControllerBase
    {
        [Route("register")]
        public IActionResult Index([FromBody] Person person)
        {
            if (!ModelState.IsValid) 
            {
                var errors = new List<string>();
                foreach (var value in ModelState.Values) 
                {
                    foreach (var erro in value.Errors)
                    {
                        errors.Add(erro.ErrorMessage);
                    }
                }
                string errorsStr = string.Join("\n", errors);
                return BadRequest(errorsStr);
            }
            return Content($"{person}");
        }
    }
}
