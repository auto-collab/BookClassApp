using Microsoft.EntityFrameworkCore;
using WebApplication1.DataFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add database context to main program run
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    // Add Connection string setup in appsettings.json file. Pass Key name
    builder.Configuration.GetConnectionString("DefaultConnection")
  ));

// Allows for real time updates to be seen without restarting application
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
