using _2.ECommerceOrdersApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2.ECommerceOrdersApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public ActionResult<Order> Create([FromBody] Order newOrder)
    {
        return CreatedAtAction(nameof(Get), new { id = newOrder.OrderNo }, newOrder);
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        return Ok();
    }
}
