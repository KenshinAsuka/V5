using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace V5.Models
{
    public class MvcMovieContextcs : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            ob.UseMySql(configuration["ConnectionStrings:MvcMovieContextcs"]);
        }


        public DbSet<Movie> Movie { get; set; }
    }
}
