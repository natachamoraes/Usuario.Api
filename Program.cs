using Microsoft.EntityFrameworkCore;
using Usuario.Api.Config;
using Usuario.Api.Data;
using Usuario.Api.Data.Repository;
using Usuario.Api.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<DbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

// Middleware para lidar com OPTIONS
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Append("Access-Control-Allow-Origin", new[] { "*" });
        context.Response.Headers.Append("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
        context.Response.Headers.Append("Access-Control-Allow-Headers", new[] { "Content-Type" });
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("OK");
    }
    else
    {
        await next();
    }
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();