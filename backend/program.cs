using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

var builder = WebApplication.CreateBuilder(args);

// Pobranie sekretu z AWS
var client = new AmazonSecretsManagerClient(RegionEndpoint.EUCentral1);

var request = new GetSecretValueRequest
{
    SecretId = "DbConnectionString"
};

var response = await client.GetSecretValueAsync(request);

// Ustawienie connection stringa
var connectionString = response.SecretString;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();