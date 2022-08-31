using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=> x.Id).UseIdentityColumn();
            builder.Property(x=>x.ImageUrl).IsRequired();
            builder.Property(x=>x.Title).HasMaxLength(50);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductPhoto).HasForeignKey(x=>x.ProductId);

        }
    }
}
