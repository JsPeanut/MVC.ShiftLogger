using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.ShiftLogger.Data;
using MVC.ShiftLogger.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShiftContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
{ 
    options.SignIn.RequireConfirmedAccount = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ShiftService, ShiftService>();
builder.Services.AddTransient<EmployeeService, EmployeeService>();

var app = builder.Build();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("api", "api/{controller=ShiftApi}");
});

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Manager", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string adminUsername = "El_Admin";
    string adminEmail = "admin@admin.com";
    string adminPassword = "Admin123!";
    string managerUsername = "El_Manager";
    string managerEmail = "manager@manager.com";
    string managerPassword = "Manager123!";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new ApplicationUser();
        adminUser.UserName = adminUsername;
        adminUser.Email = adminEmail;
        adminUser.EmailConfirmed = true;
        adminUser.FirstName = "El";
        adminUser.LastName = "Admin";

        await userManager.CreateAsync(adminUser, adminPassword);

        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    if (await userManager.FindByEmailAsync(managerEmail) == null)
    {
        var managerUser = new ApplicationUser();
        managerUser.UserName = managerUsername;
        managerUser.Email = managerEmail;
        managerUser.EmailConfirmed = true;
        managerUser.FirstName = "El";
        managerUser.LastName = "Manager";

        await userManager.CreateAsync(managerUser, managerPassword);

        await userManager.AddToRoleAsync(managerUser, "Manager");
    }
}

app.Run();
