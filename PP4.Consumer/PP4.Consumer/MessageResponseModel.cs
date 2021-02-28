using System;
using System.Collections.Generic;
using System.Text;

namespace PP4.Consumer
{
    public class MessageResponseModel
    {
        public Guid ExternalId { get; set; }
        public string Message { get; set; }
    }
}
