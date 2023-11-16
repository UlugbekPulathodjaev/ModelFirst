using Microsoft.EntityFrameworkCore;
using ModelFirst.Configurations;
using ModelFirst.Models;
using System.Reflection;

namespace ModelFirst.DataAccess
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<PersonCars> PersonCars { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonCarsTypeConfiguration());
        }

    }
}
