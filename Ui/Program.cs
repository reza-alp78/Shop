using Core.IdentityEntity;
using Infrastructure.DataBaseContext;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ui.Mappings;

var builder = WebApplication.CreateBuilder(args);

//connect to database
builder.Services.AddDbContext<ShopDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Enable Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
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
//user 15 not click evrything show login page 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.LoginPath = "/Account/Login";
    options.SlidingExpiration = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddSingleton<DapperContext>();

//reflections
//var assemblies = AppDomain.CurrentDomain.GetAssemblies();
//var repositoryAssembly = assemblies.First(a => a.FullName.Contains("Alp.Repository"));
var repositoryAssembly = typeof(SaveChangesAsyncService).Assembly.ExportedTypes;

var typesToRegistery = repositoryAssembly.Where(t => t.Namespace.StartsWith("Infrastructure.Services") && t.Name.Contains("Service") && t.IsClass);

foreach (var typeToRegister in typesToRegistery)
{
    builder.Services.AddScoped(typeToRegister.GetInterfaces()[0], typeToRegister);
}

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddMvc();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Account}/{action=Login}/{id?}"
   );

app.Run();
