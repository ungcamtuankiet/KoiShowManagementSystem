using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.Service;

using KoiShowManagementSystem.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
//builder.Services.AddScoped<ICompetitionRepository, CompetitionRepository>();




builder.Services.AddDistributedMemoryCache(); // D�ng ?? l?u tr? Session trong b? nh?
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Th?i gian h?t h?n c?a Session
    options.Cookie.HttpOnly = true; // Ch? c� th? truy c?p Session qua HTTP, kh�ng qua JavaScript
    options.Cookie.IsEssential = true; // Cho ph�p Session ngay c? khi ng??i d�ng kh�ng ch?p nh?n cookie
});
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
builder.Services.AddScoped<ICompetitionRepository, CompetitionRepository>();

// DI Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKoiService, KoiService>();
builder.Services.AddScoped<CompetitionService, CompetitionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// K�ch ho?t Session
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.MapRazorPages();

app.Run();
