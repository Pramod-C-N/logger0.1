using CRUDApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUDApplication.DAL
{
    public class MyAppDbContext : DbContext
    {

        private readonly IConfiguration configuration;

        public MyAppDbContext(IConfiguration config)
        {
            configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }


        public DbSet<Product> Products { get; set; }
    }
}
