using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

var AuthenticationPolicies = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new AuthorizeFilter(AuthenticationPolicies));
});

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<User>(config => {
    config.User.RequireUniqueEmail = true;
    config.Password.RequireDigit = true;
    config.Password.RequiredUniqueChars = 0;
    config.Password.RequireLowercase = true;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.Password.RequiredLength = 8;
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    config.Lockout.MaxFailedAccessAttempts = 5;
    config.Lockout.AllowedForNewUsers = true;
})
    .AddDefaultTokenProviders()
    .AddErrorDescriber<ErrorMessagesIdentityHelper>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, options => {
    options.LoginPath = "/Users/Login";
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Users/NotAuthorized";
    options.AccessDeniedPath = "/Users/NotAuthorized";
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();