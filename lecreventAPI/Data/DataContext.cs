using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

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
        public DbSet<UserSettings> user_settings { get; set; }
        public DbSet<Event> event_details { get; set; }
        public DbSet<UserEvent> user_events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSettings)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.userId);

            modelBuilder.Entity<Event>()
                            .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.userId);
        }
    }

}