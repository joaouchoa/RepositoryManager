using ABC.RepositoryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABC.RepositoryManager.Infrastructure.Config
{
    public class FavoriteReposConfig : IEntityTypeConfiguration<Repo>
    {
        public void Configure(EntityTypeBuilder<Repo> builder)
        {
            builder.ToTable("Repositories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Repo.MAX_LENGHT);

            builder.Property(x => x.Url)
                .IsRequired();

            builder.Property(x => x.Owner)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.Language)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Stargazers)
                .IsRequired();

            builder.Property(x => x.Forks)
                .IsRequired();

            builder.Property(x => x.Watchers)
                .IsRequired();
        }
    }
}
