using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

public class Main
{
    public IConfiguration Configuration { get; }
    public Main(IConfiguration configuration) { Configuration = configuration; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddControllersWithViews();
        services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
        });

        // Rotativa setup (add this line)
        Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");

        // Database seeding
        // Database seeding
        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
            if (!db.Users.Any(u => u.Role == "SuperAdmin"))
            {
                db.Users.Add(new User { Username = "umar", Password = "password123", Role = "SuperAdmin" });
                db.SaveChanges();
            }

            // Add missing Sections
            var sectionNames = new[] { "A", "B", "C", "E" };
            foreach (var name in sectionNames)
            {
                if (!db.Sections.Any(s => s.Name == name))
                    db.Sections.Add(new Section { Name = name });
            }

            // Add missing Sessions
            var sessionNames = new[] { "2021", "2022", "2023", "2024" };
            foreach (var name in sessionNames)
            {
                if (!db.Sessions.Any(s => s.Name == name))
                    db.Sessions.Add(new Session { Name = name });
            }

            db.SaveChanges();
        }
    }
}