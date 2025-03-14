using Microsoft.EntityFrameworkCore;
using Usuario.Api.Data;
using Usuario.Api.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<DbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

builder.Services.AddScoped<UserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();