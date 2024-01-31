using Microsoft.EntityFrameworkCore;
using mmApi.Model;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        policy => {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
        }
    );
});

var connectionString = builder.Configuration.GetConnectionString("timeTable") ?? "Data Source=timeTable.db";
builder.Services.AddSqlite<timeTableDb>(connectionString);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
