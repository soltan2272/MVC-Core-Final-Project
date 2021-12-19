using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(i => i.Adrress).IsRequired()
                .HasMaxLength(500);
            builder.Property(i => i.Full_Name).IsRequired()
                .HasMaxLength(150);
            builder.Property(i => i.Email).IsRequired()
               .HasMaxLength(300);
            builder.Property(i => i.SSN)
               .HasMaxLength(12);
            builder.Property(i => i.Date_Of_Birth)
                .HasMaxLength(50);
            builder.Property(i => i.Phone)
               .HasMaxLength(11);
        }
    }
}
