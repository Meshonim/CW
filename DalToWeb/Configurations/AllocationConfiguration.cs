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
    public class AllocationConfiguration : EntityTypeConfiguration<Allocation>
    {
        public AllocationConfiguration()
        {
            Property(c => c.AllocationValue)
                            .IsRequired()
                            .HasMaxLength(255)
                            .HasColumnAnnotation(
                            IndexAnnotation.AnnotationName,
                            new IndexAnnotation(
                            new IndexAttribute("IX_AllocationValue") { IsUnique = true }));

            Property(h => h.AllocationDate)
                .IsRequired();

            HasMany<Exemplar>(h => h.Exemplars)
                .WithRequired(s => s.Allocation)
                .HasForeignKey<int>(s => s.AllocationId);

            ToTable("Allocation");
        } 
    }
}
