using System.Runtime.Loader;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExamManagmentSystem.Helpers;
using ExamManagmentSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Register PDF Converter (DinkToPdf)
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "DinkToPdfLib", "libwkhtmltox.dll"));

// Register MVC services
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext and Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Authentication Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Register Custom Claims Factory
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();

// Configure Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Generate individual permission policies dynamically
    foreach (var permission in RolePermissions.AllPermissions)
    {
        options.AddPolicy($"Permission.{permission}", policy =>
            policy.RequireClaim("Permission", permission));
    }

    // Grouped policy examples
    options.AddPolicy("GeneratePDF", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "GenerateAttendancePDF") ||
            context.User.HasClaim("Permission", "GenerateSittingPlanPDF")));

    options.AddPolicy("AdminAccess", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "ManageRooms") &&
            context.User.HasClaim("Permission", "ManageStudents")));
});

builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Error handling for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// Enable Authentication and Authorization
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Default route → role-based redirect logic happens in DashboardController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//Optional: Seed default roles and super admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Seed.SeedRolesAndSuperAdminAsync(app);
}

app.Run();
