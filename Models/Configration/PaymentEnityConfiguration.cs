using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PaymentEnityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Payment_Type).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Card_Name).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Credit_Number).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Ccv).IsRequired().HasMaxLength(10);
            builder.Property(i => i.Expire_Date).IsRequired().HasMaxLength(10);
            builder.Property(i => i.Amount).IsRequired().HasMaxLength(10);
        }
    }
}
