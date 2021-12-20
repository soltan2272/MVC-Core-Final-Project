using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configration
{
  public  class ProdOfferConfig : IEntityTypeConfiguration<ProductOffer>
    {
        public void Configure(EntityTypeBuilder<ProductOffer> builder)
        {
            builder.ToTable("ProductOffer");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();



        }
    }
}
