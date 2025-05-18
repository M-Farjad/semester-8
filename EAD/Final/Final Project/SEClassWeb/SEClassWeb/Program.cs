var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    }
);
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=viewproduct}/{id?}");

app.Run();
