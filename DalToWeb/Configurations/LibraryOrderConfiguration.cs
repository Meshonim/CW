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
    public class LibraryOrderConfiguration : EntityTypeConfiguration<LibraryOrder>
    {
        public LibraryOrderConfiguration()
        {
            Property(h => h.LibraryOrderCount)
                .IsRequired();

            Property(h => h.LibraryOrderStatus)
                .IsRequired();

            Property(h => h.EditionId)
                .IsRequired();

            ToTable("LibraryOrder");
        }
    }
}
