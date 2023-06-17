using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly RabbitMQProducer _producer;
        private readonly RabbitMQConsumer _consumer;

        public MessageController()
        {
            _producer = new RabbitMQProducer();
            _consumer = new RabbitMQConsumer();
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] string message)
        {
            _producer.PublishMessage(message);
            return Ok();
        }

        [HttpGet]
        public IActionResult StartConsuming()
        {
            _consumer.StartConsuming();
            return Ok();
        }
    }
}