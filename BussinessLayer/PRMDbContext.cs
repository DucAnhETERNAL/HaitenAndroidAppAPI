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
    }
}
