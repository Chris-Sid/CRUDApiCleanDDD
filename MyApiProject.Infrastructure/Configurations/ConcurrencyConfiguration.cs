using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApiProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Configurations
{
    public static class ConcurrencyConfiguration
    {
        public static void ApplyRowVersion<T>(this EntityTypeBuilder<T> builder)
            where T : ConcurrentEntity
        {
            builder.Property(e => e.ConcurrencyToken)
                   .HasColumnName("xmin")
                   .HasColumnType("xid")
                   .ValueGeneratedOnAddOrUpdate()
                   .IsConcurrencyToken();
        }
    }
}
