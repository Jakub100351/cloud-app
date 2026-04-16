using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;



var builder = WebApplication.CreateBuilder(args);
var client = new AmazonSecretsManagerClient(RegionEndpoint.EUCentral1);

var request = new GetSecretValueRequest
{
    SecretId = "DbConnectionString"
};

var response = await client.GetSecretValueAsync(request);

string connectionString = response.SecretString;

builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlServer(connectionString));



builder.Services.AddControllers();



var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();