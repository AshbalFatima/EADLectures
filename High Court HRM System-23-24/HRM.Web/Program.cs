using AspNetCoreHero.ToastNotification;
using HRM.Models;
using HRM.Repositories;
using HRM.Repositories.Implementations;
using HRM.Repositories.Interfaces;

using HRM.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
ServiceLifetime.Transient
);

//builder.Services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options => {
        options.SignIn.RequireConfirmedAccount = false;
    }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>{
    options.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
    //options.Cookie.Name = "RT_LOGIN";
    //options.Cookie.HttpOnly = true;
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = new PathString( "/Identity/Account/Login");
    options.ReturnUrlParameter = "/Employee/PersonalDetail/Index";
    //options.SlidingExpiration = true;
});
//builder.Services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", config => {
//    config.Cookie.Name = "UserLoginCookie";
//    config.LogoutPath = "/Identity/Account/Login";
//});

builder.Services.AddAuthorization(config =>
{
    //var userAuthPolicyBuilder = new AuthorizationPolicyBuilder();
    //config.DefaultPolicy = userAuthPolicyBuilder.RequireAuthenticatedUser().RequireClaim(System.Security.Claims.ClaimTypes.Email).Build();
    config.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    config.AddPolicy("HRPolicy", policy => policy.RequireRole("HR"));
});
//builder.Services.AddScoped<I,UserManager<ApplicationUser>>
builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.User.RequireUniqueEmail = true;
        
        options.ClaimsIdentity.UserNameClaimType = new ClaimsIdentityOptions().RoleClaimType;



    }
);
builder.Services.AddMvcCore(options => {
    //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(WebsiteRoles.HR,WebsiteRoles.Admin,WebsiteRoles.User,WebsiteRoles.AR).Build();
    //options.Filters.Add(new AuthorizeFilter(policy));
});
//builder.Services.ConfigureApplicationCookie(options => 
//{
//    options.LoginPath = $"/Identity/Account/Login";
//    options.LogoutPath = $"/Identity/Account/Logout";
//    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
//});


builder.Services.AddScoped<IDbIntializer, DbIntializer>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddTransient<ILOVsService, LOVsService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IQualificationSerivce, QualficationService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IServiceHistory, ServiceHistoryService>();
builder.Services.AddTransient<ILHCData, LHCDataService>();
builder.Services.AddTransient<IBranchService, BranchService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddRazorPages().AddMvcOptions(options => {
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        _ => "The field is required");
});

//Nofitication 
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




 app.UseHttpsRedirection();
app.UseStaticFiles();
DataSeeding();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.MapControllerRoute(
    
    name: "default",
    pattern: "{Area=Employee}/{controller=PersonalDetail}/{action=Index}/{id?}");
    
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

app.Run();
void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
        dbIntializer.Intialize();
    }
}