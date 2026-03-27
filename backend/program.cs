using Backend.Data;

using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext<AppDbContext>(options =>

    options.UseSqlServer(connectionString));



builder.Services.AddControllers();



var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();