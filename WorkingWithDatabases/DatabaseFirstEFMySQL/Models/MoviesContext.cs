﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFMoviesDatabaseFirst.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieCast> MovieCasts { get; set; } = null!;
        public virtual DbSet<MovieCrew> MovieCrews { get; set; } = null!;
        public virtual DbSet<MovieGenre> MovieGenres { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; } = null!;
        public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=movies;user=root;password=password", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(200)
                    .HasColumnName("department_name");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("gender_id");

                entity.Property(e => e.Gender1)
                    .HasMaxLength(20)
                    .HasColumnName("gender");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("genre_id");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(100)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movie");

                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("movie_id");

                entity.Property(e => e.Budget).HasColumnName("budget");

                entity.Property(e => e.Homepage)
                    .HasMaxLength(1000)
                    .HasColumnName("homepage");

                entity.Property(e => e.MovieStatus)
                    .HasMaxLength(50)
                    .HasColumnName("movie_status");

                entity.Property(e => e.Overview)
                    .HasMaxLength(1000)
                    .HasColumnName("overview");

                entity.Property(e => e.Popularity)
                    .HasPrecision(12, 6)
                    .HasColumnName("popularity");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.Revenue).HasColumnName("revenue");

                entity.Property(e => e.Runtime).HasColumnName("runtime");

                entity.Property(e => e.Tagline)
                    .HasMaxLength(1000)
                    .HasColumnName("tagline");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title");

                entity.Property(e => e.VoteAverage)
                    .HasPrecision(4, 2)
                    .HasColumnName("vote_average");

                entity.Property(e => e.VoteCount).HasColumnName("vote_count");

                entity.HasMany(d => d.Companies)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieCompany",
                        l => l.HasOne<ProductionCompany>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_movie_company_production_company"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_movie_company_movie1"),
                        j =>
                        {
                            j.HasKey("MovieId", "CompanyId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("movie_company");

                            j.HasIndex(new[] { "CompanyId" }, "FK_movie_company_production_company");

                            j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");

                            j.IndexerProperty<int>("CompanyId").HasColumnName("company_id");
                        });
            });

            modelBuilder.Entity<MovieCast>(entity =>
            {
                entity.ToTable("movie_cast");

                entity.HasIndex(e => e.MovieId, "FK_movie_cast_movie");

                entity.HasIndex(e => e.GenderId, "movie_cast$fk_gender");

                entity.HasIndex(e => e.PersonId, "movie_cast$fk_person_2");

                entity.Property(e => e.MovieCastId)
                    .ValueGeneratedNever()
                    .HasColumnName("movie_cast_id");

                entity.Property(e => e.CastOrder).HasColumnName("cast_order");

                entity.Property(e => e.CharacterName)
                    .HasMaxLength(400)
                    .HasColumnName("character_name");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.MovieCasts)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("movie_cast$fk_gender");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieCasts)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_movie_cast_movie");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.MovieCasts)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("movie_cast$fk_person_2");
            });

            modelBuilder.Entity<MovieCrew>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("movie_crew");

                entity.HasIndex(e => e.MovieId, "FK_movie_crew_movie");

                entity.HasIndex(e => e.DepartmentId, "movie_crew$fk_department");

                entity.HasIndex(e => e.PersonId, "movie_crew$fk_person");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Job)
                    .HasMaxLength(200)
                    .HasColumnName("job");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("movie_crew$fk_department");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_movie_crew_movie");

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("movie_crew$fk_person");
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.ToTable("movie_genres");

                entity.HasIndex(e => e.MovieId, "FK_movie_genres_movie");

                entity.HasIndex(e => e.GenreId, "movie_genres$fk_mg_genre");

                entity.Property(e => e.MovieGenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("movie_genre_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_genres$fk_mg_genre");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_genres_movie");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("person_id");

                entity.Property(e => e.PersonName)
                    .HasMaxLength(500)
                    .HasColumnName("person_name");
            });

            modelBuilder.Entity<ProductionCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.ToTable("production_company");

                entity.Property(e => e.CompanyId)
                    .ValueGeneratedNever()
                    .HasColumnName("company_id");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .HasColumnName("company_name");
            });

            modelBuilder.Entity<Sysdiagram>(entity =>
            {
                entity.HasKey(e => e.DiagramId)
                    .HasName("PRIMARY");

                entity.ToTable("sysdiagrams");

                entity.HasIndex(e => new { e.PrincipalId, e.Name }, "UK_principal_name")
                    .IsUnique();

                entity.Property(e => e.DiagramId)
                    .ValueGeneratedNever()
                    .HasColumnName("diagram_id");

                entity.Property(e => e.Definition).HasColumnName("definition");

                entity.Property(e => e.Name)
                    .HasMaxLength(160)
                    .HasColumnName("name");

                entity.Property(e => e.PrincipalId).HasColumnName("principal_id");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
