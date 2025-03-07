using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using BussinessLayer;
using System.IO;

namespace BussinessObject
{
    public class PRMDbContext : DbContext
    {
        public PRMDbContext(DbContextOptions<PRMDbContext> options)
            : base(options)
        {
        }

        public PRMDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterImages> ChapterImages { get; set; }
        public DbSet<ChapterText> ChapterTexts { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<UserMangaList> UserMangaLists { get; set; }
        public DbSet<ReadingHistory> ReadingHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

                if (File.Exists(configPath))
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.CommandError));
                }
                else
                {
                    throw new FileNotFoundException("Không tìm thấy appsettings.json!");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: Manga - Chapter (Cascade Delete)
            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.Manga)
                .WithMany(m => m.Chapters)
                .HasForeignKey(c => c.MangaId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-One: Chapter - ChapterText (Cascade Delete)
            modelBuilder.Entity<ChapterText>()
                .HasOne(ct => ct.Chapter)
                .WithOne(c => c.ChapterText)
                .HasForeignKey<ChapterText>(ct => ct.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Chapter - ChapterImages (Cascade Delete)
            modelBuilder.Entity<ChapterImages>()
                .HasOne(ci => ci.Chapter)
                .WithMany(c => c.ChapterImages)
                .HasForeignKey(ci => ci.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: User - ReadingHistory (Cascade Delete)
            modelBuilder.Entity<ReadingHistory>()
                .HasOne(rh => rh.User)
                .WithMany(u => u.ReadingHistories)
                .HasForeignKey(rh => rh.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Chapter - ReadingHistory (Cascade Delete)
            modelBuilder.Entity<ReadingHistory>()
                .HasOne(rh => rh.Chapter)
                .WithMany(c => c.ReadingHistories)
                .HasForeignKey(rh => rh.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            // ❌ Ngăn chặn Cascade Delete trên MangaId trong ReadingHistory
            modelBuilder.Entity<ReadingHistory>()
                .HasOne(rh => rh.Manga)
                .WithMany(m => m.ReadingHistories)
                .HasForeignKey(rh => rh.MangaId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Thay vì Cascade

            // One-to-Many: Manga - Rate (Cascade Delete)
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.Manga)
                .WithMany(m => m.Rates)
                .HasForeignKey(r => r.MangaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rate>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many: User - Manga (No Cascade)
            modelBuilder.Entity<UserMangaList>()
                .HasOne(uml => uml.User)
                .WithMany(u => u.UserMangaLists)
                .HasForeignKey(uml => uml.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Ngăn chặn cascade delete

            modelBuilder.Entity<UserMangaList>()
                .HasOne(uml => uml.Manga)
                .WithMany(m => m.UserMangaLists)
                .HasForeignKey(uml => uml.MangaId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Ngăn chặn cascade delete
        }

    }
}
