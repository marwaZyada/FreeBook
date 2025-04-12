using AutoMapper;
using Book.Permission;
using Book.Settings;
using Domain.Entity;
using Domain.Entity.Identity;

using Infrastructure.Data;
using Infrastructure.IRepositories;
using Infrastructure.IRepositories.ServicesRepository;
using Infrastructure.Seeds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewOptions(options => {
    options.HtmlHelperOptions.ClientValidationEnabled = true;
}); 
builder.Services.AddDbContext<FreeBookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
  
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
}).AddEntityFrameworkStores<FreeBookDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin";
    options.AccessDeniedPath= "/Admin/Home/Denied";
});
builder.Services.AddScoped<IservicesRepository<Category>, ServicesCategory>();
builder.Services.AddScoped<IServicesRepositoryLog<LogCategory>, ServicesCategoryLog>();
builder.Services.AddSession();
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

var app = builder.Build();
//update database
using var scope = app.Services.CreateScope();
var services= scope.ServiceProvider;
try
{
    var usermanager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var rolemanager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedRoleAsync(rolemanager);
    await DefaultUsers.SeedUsersAsync(usermanager, rolemanager);
}
catch (Exception)
{

    throw;
}

//var dbcontext = services.GetRequiredService<FreeBookDbContext>();
//await dbcontext.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}"
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
