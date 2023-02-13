using AutoMapper;
using Employees.Api.Context;
using Employees.Api.Mapper;
using Employees.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connection db
builder.Services.AddDbContext<EmployeeContext>(options =>
     options.UseSqlite(builder.Configuration.GetConnectionString("AppConnection")));

builder.Services.AddTransient<IEmployeeContext, EmployeeContext>();
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();

//automapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapping());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
