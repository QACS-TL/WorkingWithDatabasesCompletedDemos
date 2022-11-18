using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace EFCodeFirstFilmReviewsDB.Models
{
    internal class FilmReviewContext : DbContext
    {
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _connectionString = @"server=127.0.0.1; port=3306; database=test; user=root; password=password;";
            //optionsBuilder.UseSqlServer(@"Server=(local); Initial Catalog=FilmReviews; trusted_connection=true");
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }
    }
}
