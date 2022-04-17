using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.Data;
using Shop.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppEFContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.Configure<FormOptions>(options =>
{
    // Set the limit to 256 MB
    options.ValueCountLimit = 1024;
    options.KeyLengthLimit = 1024 * 2;
    options.ValueLengthLimit = 1024 * 1024 * 100;
});

var app = builder.Build();

app.UseCors(options =>
                options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
});


app.UseAuthorization();

app.MapControllers();

app.Run();
