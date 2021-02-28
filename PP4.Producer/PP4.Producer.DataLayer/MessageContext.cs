using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PP4.Producer.DataLayer
{
    public class MessageContext : DbContext
    {
        public DbSet<MessagePm> Messages { get; set; }
        public MessageContext() : base()
        {
            Database.EnsureCreated();
        }

        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=PP4Producer;Trusted_Connection=True");
        }

    }
}
