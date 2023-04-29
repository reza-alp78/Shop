using Core.IdentityEntity;
using Infrastructure.DataBaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//connect to database
builder.Services.AddDbContext<ShopDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Enable Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option => {
    option.User.RequireUniqueEmail = true;
    option.SignIn.RequireConfirmedEmail = true;
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireDigit = true;
    option.Password.RequiredUniqueChars = 1;
}).AddEntityFrameworkStores<ShopDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ShopDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ShopDbContext, Guid>>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
