using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieProNet6.Data;
using MovieProNet6.Models.Settings;
using MovieProNet6.Services;
using MovieProNet6.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();;

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(ConnectionService.GetConnectionString(builder.Configuration)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddTransient<SeedService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRemoteMovieService, TMDBMovieService>();
builder.Services.AddScoped<IDataMappingService, TMDBMappingService>();
builder.Services.AddSingleton<IImageService, BasicImageService>();

////Mathieu:
//builder.Services.AddScoped<MovieSearchService>();

var app = builder.Build();

var dataService = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedService>();
await dataService.ManageDataAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
