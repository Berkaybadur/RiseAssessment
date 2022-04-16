using Microsoft.AspNetCore.Mvc;
using NewsApp.RabbitMQ.Api.Abstract;
using NewsApp.RabbitMQ.Api.Models;

namespace NewsApp.RabbitMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : Controller
    {
        private readonly IRabbitMqService _rabbitMqService;

        public JobController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        [HttpPost]
        public IActionResult Get(Contact contact)
        {
            _rabbitMqService.SendToQueue(contact);
            return Ok("Success");
        }
    }
}
