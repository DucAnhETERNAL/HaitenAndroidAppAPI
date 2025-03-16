using Repository.Repo;
using Repository;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);
// Add Scoped services for each repository

builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IChapterTextRepository, ChapterTextRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IRateRepository, RateRepository>();
builder.Services.AddScoped<IReadingHistoryRepository, ReadingHistoryRepository>();
builder.Services.AddScoped<IUserMangaListRepository, UserMangaListRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);  // Cổng HTTP
    options.ListenAnyIP(443, listenOptions =>  // Cổng HTTPS
    {
        listenOptions.UseHttps();
    });
});


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowSpecificOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
