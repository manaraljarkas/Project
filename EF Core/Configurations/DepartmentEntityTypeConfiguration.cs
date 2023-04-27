using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Configurations
{
    internal class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            //builder.Ignore("Name");
            //builder.Property(x => x.Name).HasColumnName("Name");
            //builder.Property(x=>x.Name).HasColumnType("varchar(200)");
        }
    }
}
