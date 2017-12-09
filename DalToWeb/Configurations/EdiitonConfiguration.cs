using DalToWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Configurations
{
    public class EditionConfiguration : EntityTypeConfiguration<Edition>
    {
        public EditionConfiguration()
        {
            HasMany<Genre>(s => s.Genres)
                .WithMany(c => c.Editions)
                .Map(m => m.ToTable("Edition_Genre")
                .MapLeftKey("EditionId")
                .MapRightKey("GenreId"));

            Property(c => c.EditionTitle)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.EditionYear)
                .IsRequired();

            ToTable("Edition");
        }
    }
}
