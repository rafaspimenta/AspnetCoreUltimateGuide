using _1.ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1.ControllersExample.Controllers;

public class HomeController : ControllerBase
{
    [Route("/")]
    [Route("home")]
    public ContentResult Index()
    {
        var result = new ContentResult()
        {
            Content = $"Hello World from Index",
            ContentType = "text/plain",
        };
        return result;
        //return Content($"Hello World from {nameof(Index)}", "text/plain");
        //return Content("<h1>Hello World from Home</h1>", "text/html");
    }

    [Route("person")]
    public JsonResult Person()
    {
        var rafael = new Person()
        {
            Id = Guid.NewGuid(),
            FirstName = "Rafael",
            LastName = "Pimenta",
            Age = 37
        };
        return new JsonResult(rafael);
    }

    [Route("about")]
    public string About()
    {
        return $"Hello World from {nameof(About)}";
    }

    [Route("contact/{mobile:regex(^\\d{{10}}$)}")]
    public string Contact()
    {
        return $"Hello World from {nameof(Contact)}";
    }

    [Route("file-download")]
    public VirtualFileResult FileDownload()
    {
        return new VirtualFileResult("./image.jpg", "application/jpeg");
    }

    [Route("file-download2")]
    public PhysicalFileResult FileDownload2()
    {
        var currentFolder = Directory.GetCurrentDirectory();
        var absoluteFilePath = Path.Combine(currentFolder, "wwwroot/image.jpg");

        return new PhysicalFileResult(absoluteFilePath, "image/jpeg");
    }

    [Route("file-download3")]
    public FileContentResult FileDownload3()
    {
        var currentFolder = Directory.GetCurrentDirectory();
        var absoluteFilePath = Path.Combine(currentFolder, "wwwroot/image.jpg");
        var imageBytes = System.IO.File.ReadAllBytes(absoluteFilePath);


        return new FileContentResult(imageBytes, "application/jpeg");
    }

    [Route("book/{bookid?}/{isloggedin?}")]
    public IActionResult Book(int? bookid, [FromRoute] bool? isloggedin, Book book)
    {
        if (!bookid.HasValue)
        {
            return NotFound("Book id is not supplied or empty");
        }

        if (bookid <= 0)
        {
            return BadRequest("Book cannot be less or equal to zero");
        }

        if (bookid > 1001)
        {
            return BadRequest("Book cannot be greater than 1000");
        }

        if (!isloggedin.HasValue)
        {
            return BadRequest("User id is not supplied or empty");
        }

        if (!isloggedin.Value)
        {
            return Unauthorized("User is not logged in");
        }

        return Content($"Book id: {bookid}", "text/plain");
    }
}
