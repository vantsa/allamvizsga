using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace lecreventAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230401123844_CreatedEvents")]
    partial class CreatedEvents
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("lecreventAPI.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("latitude")
                        .HasColumnType("double");

                    b.Property<double>("longitude")
                        .HasColumnType("double");

                    b.Property<int>("spaces")
                        .HasColumnType("int");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("event_details");
                });

            modelBuilder.Entity("lecreventAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("user_profiles");
                });

            modelBuilder.Entity("lecreventAPI.Models.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("rangeEnd")
                        .HasColumnType("int");

                    b.Property<int>("rangeStart")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<int>("value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("user_settings");
                });

            modelBuilder.Entity("lecreventAPI.Models.Event", b =>
                {
                    b.HasOne("lecreventAPI.Models.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("lecreventAPI.Models.UserSettings", b =>
                {
                    b.HasOne("lecreventAPI.Models.User", "User")
                        .WithMany("UserSettings")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("lecreventAPI.Models.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("UserSettings");
                });
        }
    }
}
