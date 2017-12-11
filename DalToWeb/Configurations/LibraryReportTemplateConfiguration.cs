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
    public class LibraryReportTemplateConfiguration : EntityTypeConfiguration<LibraryReportTemplate>
    {
        public LibraryReportTemplateConfiguration()
        {
            Property(c => c.TemplateName)
                .IsRequired()
                .HasMaxLength(255);
            Property(c => c.PrintHeaders)
                .IsRequired();
            Property(c => c.Autosized)
                .IsRequired();
            Property(c => c.MaxCount)
                .IsRequired();

            ToTable("LibraryReportTemplate");
        }
    }
}
