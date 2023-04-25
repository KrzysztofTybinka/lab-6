using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.QuizUserService;
using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Models;
using Infrastructure;
using Infrastructure.EF;
using Infrastructure.EF.EF.Services;
using Infrastructure.Memory.Repository;

using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IQuizUserService, QuizUserService>();
builder.Services.AddDbContext<QuizDbContext>();                             // infrastructure
builder.Services.AddTransient<IQuizUserService, QuizUserServiceEF>();       // infrastructure
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
    
app.Run();