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
    public class NewsConfiguration : EntityTypeConfiguration<News>
    {
        public NewsConfiguration()
        {
            Property(c => c.NewsTitle)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.NewsContent)
                .IsRequired()
                .HasMaxLength(500);

            ToTable("News");
        }
    }
}
