using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP4.Producer.DataLayer
{
    [Table("tbl_producer_messages")]
    public class MessagePm
    {
        [Column("message_id"), Key]
        public int Id { get; set; }
        [Column("external_id")]
        public Guid ExternalId { get; set; }
        [Column("message")]
        public string Message { get; set; }
    }
}
