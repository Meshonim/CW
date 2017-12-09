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
    public class HouseConfiguration : EntityTypeConfiguration<House>
    {
        public HouseConfiguration()
        {
            HasMany<Edition>(h => h.Editions)
                .WithRequired(s => s.House)
                .HasForeignKey<int>(s => s.HouseId);

            Property(h => h.HouseName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_HouseNameCityId", 1) { IsUnique = true }));

            Property(h => h.CityId)
                .IsRequired()
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_HouseNameCityId", 2) { IsUnique = true }));

            Property(h => h.Phone)
                .IsOptional()
                .HasMaxLength(20);

            ToTable("House");
        }
    }
}
