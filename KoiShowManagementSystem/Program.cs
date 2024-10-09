using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Realtime
builder.Services.AddSignalR();

// Db
builder.Services.AddDbContext<KoiShowManagementSystemContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(connectionString);
});

// DI Repo
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKoiRepository, KoiRepository>();

// DI Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKoiService, KoiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
