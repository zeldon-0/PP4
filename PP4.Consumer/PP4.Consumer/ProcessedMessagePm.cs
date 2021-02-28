using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PP4.Consumer
{
    [Table("tbl_processed_message")]
    public class ProcessedMessagePm
    {
        [Column("processed_message_id"), Key]
        public Guid Id { get; set; } 
        [Column("message")]
        public string Message { get; set; }
    }
}
