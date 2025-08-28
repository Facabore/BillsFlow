using BillsFlow.Api.Extensions;
using BillsFlow.Application;
using BillsFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Dependecy Injection
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();
    


app.UseHttpsRedirection();

// Custom middleware
app.UseCustomExceptionHandler();
// 
app.UseAuthorization();

app.MapControllers();

app.Run();
