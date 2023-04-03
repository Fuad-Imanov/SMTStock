using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMTstock.Entities.Models;

namespace SMTstock.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        DbSet<Product> Products { get; set; }
        DbSet<Merchant> Merchants { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderProduct> OrderProducts { get; set; }
        DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
