using ABC.RepositoryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.RepositoryManager.Infrastructure.Context
{
    public class ReposManagerContext : DbContext
    {
        public ReposManagerContext()
        {
            
        }

        public ReposManagerContext(DbContextOptions<ReposManagerContext> options) : base(options)
        {
            
        }

        public DbSet<Repo> Repos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReposManagerContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
