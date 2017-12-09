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
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            HasMany<House>(h => h.Houses)
                .WithRequired(s => s.City)
                .HasForeignKey<int>(s => s.CityId);

            Property(c => c.CityName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_CityName") { IsUnique = true }));

            ToTable("City");
        }
    }
}
