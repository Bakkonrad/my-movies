using MyMoviesApi.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyMoviesApi.Database
{
    public class MoviesDb : DbContext
    {
        public MoviesDb(DbContextOptions<MoviesDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureMovieEntity(modelBuilder.Entity<MovieEntity>());
        }

        private void ConfigureMovieEntity(EntityTypeBuilder<MovieEntity> entity)
        {
            entity.ToTable("movies");

            entity.Property(x => x.title)
                .HasColumnName("title")
                .HasColumnType("varchar(200)")
                .IsRequired();

            entity.Property(x => x.director)
                .HasColumnName("director")
                .HasColumnType("varchar(200)");

            entity.Property(x => x.year)
                .HasColumnName("year")
                .HasColumnType("int");

            entity.Property(x => x.rate)
                .HasColumnName("rate")
                .HasColumnType("int");
            
        }

        public DbSet<MovieEntity> Movies { get; set; }
    }
}
