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
    public class OrderReportTemplateConfiguration : EntityTypeConfiguration<OrderReportTemplate>
    {
        public OrderReportTemplateConfiguration()
        {
            Property(c => c.TemplateName)
                .IsRequired()
                .HasMaxLength(255);
            Property(c => c.PrintBackground)
                .IsRequired();
            Property(c => c.PrintDayOfWeek)
                .IsRequired();
            Property(c => c.PrintEditionTitle)
                 .IsRequired();
            Property(c => c.MaxCount)
                .IsRequired();

            ToTable("OrderReportTemplate");
        }
    }
}
