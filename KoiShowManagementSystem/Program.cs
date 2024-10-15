using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDistributedMemoryCache(); // Dùng ?? l?u tr? Session trong b? nh?
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Th?i gian h?t h?n c?a Session
    options.Cookie.HttpOnly = true; // Ch? có th? truy c?p Session qua HTTP, không qua JavaScript
    options.Cookie.IsEssential = true; // Cho phép Session ngay c? khi ng??i dùng không ch?p nh?n cookie
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
builder.Services.AddScoped<ICompetitionService, CompetitionService>();

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

// Kích ho?t Session
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.MapRazorPages();

app.Run();
