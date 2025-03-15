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
        public DbSet<Comment> Comments { get; set; }
        /*public DbSet<ChapterImages> ChapterImages { get; set; }*/
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
            /* modelBuilder.Entity<ChapterImages>()
                 .HasOne(ci => ci.Chapter)
                 .WithMany(c => c.ChapterImages)
                 .HasForeignKey(ci => ci.ChapterId)
                 .OnDelete(DeleteBehavior.Cascade);*/

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
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Chapter)
                .WithMany(ch => ch.Comments)
                .HasForeignKey(c => c.ChapterId);
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
            modelBuilder.Entity<Genres>().HasData(
        new Genres { Id = 1, Name = "Action" },
        new Genres { Id = 2, Name = "Adventure" },
        new Genres { Id = 3, Name = "Romance" }
    );

            // Seed Manga
            modelBuilder.Entity<Manga>().HasData(
                new Manga
                {
                    Id = 1,
                    Title = "Manga 1",
                    Description = "Description for Manga 1",
                    Author = "Author 1",
                    Type = "Type 1",
                    ImageUrls = "url1",
                    GenreId = 1,
                    Status = "Active"
                },
                new Manga
                {
                    Id = 2,
                    Title = "Manga 2",
                    Description = "Description for Manga 2",
                    Author = "Author 2",
                    Type = "Type 2",
                    ImageUrls = "url2",
                    GenreId = 2,
                    Status = "Active"
                }
            );

            // Seed Chapters
            modelBuilder.Entity<Chapter>().HasData(
                new Chapter
                {
                    Id = 1,
                    MangaId = 1,
                    Name = "Chapter 1",
                    Status = true,
                    ViewCount = 100,
                    CreatedAt = new DateTime(2023, 1, 1)
                },
                new Chapter
                {
                    Id = 2,
                    MangaId = 2,
                    Name = "Chapter 1",
                    Status = true,
                    ViewCount = 150,
                    CreatedAt = new DateTime(2023, 1, 1)    
                }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "user1",
                    Email = "user1@example.com",
                    Password = "password1", // Make sure to hash passwords before storing them
                    Role = "User",
                    Status = "Active"
                },
                new User
                {
                    Id = 2,
                    UserName = "admin1",
                    Email = "admin1@example.com",
                    Password = "password2", // Hash password here
                    Role = "Admin",
                    Status = "Active"
                }
            );

            // Seed UserMangaList (Many-to-Many)
            modelBuilder.Entity<UserMangaList>().HasData(
                new UserMangaList
                {
                    Id = 1,
                    UserId = 1,
                    MangaId = 1,
                    AddedAt = new DateTime(2023, 1, 1),
                    IsFavorite = true
                },
                new UserMangaList
                {
                    Id = 2,
                    UserId = 2,
                    MangaId = 2,
                    AddedAt = new DateTime(2023, 1, 1),
                    IsFavorite = false
                }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    UserId = 1,
                    ChapterId = 1,
                    Content = "Great chapter!",
                    CreatedAt = new DateTime(2023, 1, 1)
                }
            );

            // Seed ReadingHistory
            modelBuilder.Entity<ReadingHistory>().HasData(
                new ReadingHistory
                {
                    Id = 1,
                    UserId = 1,
                    MangaId = 1,
                    ChapterId = 1,
                    ReadDate = new DateTime(2023, 1, 1),
                    Status = "Completed"
                }
            );

            // Seed Rates
            modelBuilder.Entity<Rate>().HasData(
                new Rate
                {
                    Id = 1,
                    MangaId = 1,
                    UserId = 1,
                    Rating = 5,
                    Comment = "Excellent!",
                    CreatedAt = new DateTime(2023, 1, 1)
                }
            );
        }

    }
}
