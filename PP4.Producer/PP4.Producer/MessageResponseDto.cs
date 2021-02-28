using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP4.Producer
{
    public class MessageResponseDto
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Message { get; set; }
    }
}
