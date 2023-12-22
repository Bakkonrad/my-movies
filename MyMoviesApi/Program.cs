using Microsoft.EntityFrameworkCore;
using MovieApp;
using MyMoviesApi.Database;
using Newtonsoft.Json.Serialization;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// var credential = new Azure.Identity.DefaultAzureCredential();

// builder.Configuration.AddAzureKeyVault(
//     new Uri("https://mymovieskeyvault.vault.azure.net/"), credential);

builder.Services.AddDbContext<MoviesDb>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesDb");
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JSON Serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddTransient<IMoviesService, MovieService>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
