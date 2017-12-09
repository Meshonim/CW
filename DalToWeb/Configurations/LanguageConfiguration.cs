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
    public class LanguageConfiguration : EntityTypeConfiguration<Language>
    {
        public LanguageConfiguration()
        {
            HasMany<Edition>(e => e.Editions)
                .WithRequired(s => s.Language)
                .HasForeignKey<short>(s => s.LanguageId);

            Property(c => c.LanguageName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_LanguageName") { IsUnique = true }));

            ToTable("Language");
        }
    }
}
