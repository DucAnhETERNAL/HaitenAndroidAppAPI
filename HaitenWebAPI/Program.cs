using Repository.Repo;
using Repository;
using Microsoft.AspNetCore.OData;
using Net.payOS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BussinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Repo;
using System;

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

builder.Services.AddSingleton(new PayOS("295a3346-3eeb-449c-bb7b-cdbf495577ec", "a5e3d88f-3ae6-4235-b30e-e81c2b3686a2", "2a895d2b7938d4880973602f579a44043a2bc63183aa80e685ace2e9164cab5f"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("SpecificOrigin",
        build =>
        {
            build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PRMDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<ChapterImagesDAO>();
builder.Services.AddScoped<IChapterImagesRepository, ChapterImagesRepository>();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("SpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
