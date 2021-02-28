using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PP4.Consumer
{
    public class ProcessedMessageContext : DbContext
    {
        public DbSet<ProcessedMessagePm> Messages { get; set; }
        public ProcessedMessageContext() : base()
        {
            Database.EnsureCreated();
        }

        public ProcessedMessageContext(DbContextOptions<ProcessedMessageContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=PP4Consumer;Trusted_Connection=True");
        }

    }
}
