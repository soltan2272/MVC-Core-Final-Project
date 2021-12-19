using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Description).IsRequired()
                .HasMaxLength(1000);
            builder.Property(i => i.Name).IsRequired()
                .HasMaxLength(150);
            builder.Property(i => i.Price).IsRequired()
               .HasMaxLength(20);
          /*  builder.Property(i => i.Image).IsRequired()
              .HasMaxLength(500);*/
            builder.Property(i => i.Rate).IsRequired()
               .HasMaxLength(5);
           
        }
    }
}
