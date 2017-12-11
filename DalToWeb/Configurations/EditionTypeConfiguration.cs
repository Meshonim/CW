using DalToWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Configurations
{
    public class EditionTypeConfiguration : EntityTypeConfiguration<EditionType>
    {
        public EditionTypeConfiguration()
        {
            Property(h => h.EditionTypeName)
                .IsRequired()
                .HasMaxLength(50);

            Property(h => h.EditionTypeDescription)
                .IsRequired()
                .HasMaxLength(255);

            HasMany<Edition>(h => h.Editions)
                .WithRequired(s => s.EditionType)
                .HasForeignKey<int>(s => s.EditionTypeId);

            ToTable("EditionType");
        }
    }
}
