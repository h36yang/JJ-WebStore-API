﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public class ProductCategoryMap : BaseEntityMap<ProductCategory>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasData(
                new ProductCategory() { Id = 1, Name = "Featured", IsActive = true, CreatedOn = DateTimeOffset.Now, UpdatedOn = DateTimeOffset.Now },
                new ProductCategory() { Id = 2, Name = "Hot", IsActive = true, CreatedOn = DateTimeOffset.Now, UpdatedOn = DateTimeOffset.Now }
            );
        }
    }
}
