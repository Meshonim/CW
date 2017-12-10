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
    public class ReaderOrderConfiguration : EntityTypeConfiguration<ReaderOrder>
    {
        public ReaderOrderConfiguration()
        {
            Property(h => h.ExemplarId)
                .IsRequired();

            Property(h => h.UserId)
                .IsRequired();

            ToTable("ReaderOrder");
        }
    }
}
