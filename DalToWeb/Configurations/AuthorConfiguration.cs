using DalToWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Configurations
{
    public class AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(c => c.AuthorLastName)
                .IsRequired();
            Property(c => c.AuthorFirstName)
                .IsRequired();

            ToTable("Author");
        }
    }
}
