using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  /* public class StoreEntityConfiguration:IEntityTypeConfiguration<Store>
    {
       public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Name).IsRequired()
                .HasMaxLength(150);
            builder.Property(i => i.Address).IsRequired()
                .HasMaxLength(500);
            builder.Property(i => i.Phone).IsRequired()
               .HasMaxLength(15);
        }
    }*/
}
