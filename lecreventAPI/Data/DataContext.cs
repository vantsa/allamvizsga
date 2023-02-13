using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


namespace lecreventAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            ///optionsBuilder.UseMySql("server=localhost;user=root;database=lecrevent;password=;port=3306");
        }

        public DbSet<User> user_profiles { get; set; }
    }
}