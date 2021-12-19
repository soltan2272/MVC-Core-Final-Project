using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offer");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Start_Date).IsRequired()
                .HasMaxLength(50);
            builder.Property(i => i.End_Date).IsRequired()
                 .HasMaxLength(50);
            builder.Property(i => i.Discount_Percentage).IsRequired()
               .HasMaxLength(15);
          
        }

      
    }
}
