using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TesteLarTech.Domain.Entities;

namespace TesteLarTech.Core.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
            .HasMany(p => p.Telefones)
            .WithOne(t => t.Pessoa)
            .HasForeignKey(t => t.IdPessoa);

            modelBuilder.Entity<Telefone>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Telefone>()
                .HasOne(t => t.Pessoa)
                .WithMany(p => p.Telefones)
                .HasForeignKey(t => t.IdPessoa);

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}