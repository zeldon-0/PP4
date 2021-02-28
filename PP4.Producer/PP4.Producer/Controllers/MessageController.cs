using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PP4.Producer.DataLayer;
using RabbitMQ.Client;

namespace PP4.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageContext _messageContext;
        public MessageController(MessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage(MessageDto messageDto)
        {
            var rabbitMqClient = new RabbitMqClient();
            var serializedMessage = JsonConvert.SerializeObject(messageDto);
            var response = rabbitMqClient.Call(serializedMessage);
            rabbitMqClient.Close();
            MessagePm deserializedResponse;
            try
            {
                deserializedResponse = JsonConvert.DeserializeObject<MessagePm>(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            _messageContext.Messages.Add(deserializedResponse);
            await _messageContext.SaveChangesAsync();

            var messageResponseDto = new MessageResponseDto
            {
                Id = deserializedResponse.Id,
                ExternalId = deserializedResponse.ExternalId,
                Message = deserializedResponse.Message
            };
            return Ok(messageResponseDto);
        }
    }
}
