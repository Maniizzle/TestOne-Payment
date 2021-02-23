using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentApplication.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Persistence.Context
{
    public class PaymentAppDbContext:DbContext
    {
        public DbSet<PaymentLog> PaymentLog { get; set; }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }


        public PaymentAppDbContext(DbContextOptions<PaymentAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentLog>()
                      .Property(p => p.PaymentState)
                      .HasConversion(
                          new EnumToStringConverter<PaymentState>());

            modelBuilder.Entity<PaymentLog>()
                     .Property(p => p.PaymentReference)
                     .HasConversion(
                         new EnumToStringConverter<PaymentGateway>());
        }
    }
}
