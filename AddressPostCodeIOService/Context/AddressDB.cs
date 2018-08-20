using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AddressPostCodeIOService.Configuration;

namespace AddressPostcodeIOService.Context
{
    public class AddressDB : DbContext
    {
        public static IConfiguration Configuration { get; private set; }

        // Reference the Address table using this
        public DbSet<Models.Address> Address { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string conString = Microsoft
            //    .Extensions
            //    .Configuration
            //    .ConfigurationExtensions
            //    .GetConnectionString(Configuration, "DBConnection");

            string conString = Startup.ConnectionString;
            
            optionsBuilder.UseSqlServer(conString);
            //optionsBuilder.UseSqlServer("Server=DEV-SQL2016;Database=AddressPostcodeDB;Persist Security Info=true;Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Models.Address>(entity =>
            //{
            //    entity.Property(e => e.Postcode).IsRequired();
            //});

        }
    }
}

