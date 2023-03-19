using Microsoft.EntityFrameworkCore;


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
        }

        public DbSet<User> user_profiles { get; set; }
        public DbSet<UserSettings> user_settings {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSettings)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.userId);
        }
    }
}