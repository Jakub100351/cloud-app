using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// --- ZAKLĘCIE NA CORS (Pozwala Reactowi gadać z API) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// -------------------------------------------------------

// ZADANIE 4.2: Połączenie z kontenerem bazy danych
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=db;Port=5432;Database=TaskDb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// --- ODPALAMY CORS ---
app.UseCors("AllowReact");
// ---------------------

// To upewni się, że baza danych zostanie automatycznie stworzona po uruchomieniu!
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<TaskItem> Tasks { get; set; }
}

public class TaskItem {
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}