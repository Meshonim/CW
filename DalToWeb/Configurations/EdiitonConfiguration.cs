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

            HasMany<Author>(s => s.Authors)
                .WithMany(c => c.Editions)
                .Map(m => m.ToTable("Edition_Author")
                .MapLeftKey("EditionId")
                .MapRightKey("AuthorId"));

            HasMany<Author>(s => s.Translators)
                .WithMany(c => c.TranslatedEditions)
                .Map(m => m.ToTable("Edition_Translator")
                .MapLeftKey("EditionId")
                .MapRightKey("AuthorId"));

            HasMany<Author>(s => s.Illustrators)
                .WithMany(c => c.IllustratedEditions)
                .Map(m => m.ToTable("Edition_Illustrator")
                .MapLeftKey("EditionId")
                .MapRightKey("AuthorId"));

            HasMany<LibraryOrder>(h => h.LibraryOrders)
                .WithRequired(s => s.Edition)
                .HasForeignKey<int>(s => s.EditionId);

            Property(c => c.EditionTitle)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.EditionYear)
                .IsRequired();

            ToTable("Edition");
        }
    }
}
