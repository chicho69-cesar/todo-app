using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TodoApp.Controllers;
using TodoApp.Data;
using TodoApp.Helpers;
using TodoApp.Models.DTOs;
using TodoApp.Services;
using TodoApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var AuthenticationPolicies = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new AuthorizeFilter(AuthenticationPolicies));
});

builder.Services.AddDbContext<TodoAppContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<UserDTO>(config => {
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.Password.RequiredLength = 6;
})
    .AddErrorDescriber<ErrorMessagesIdentityHelper>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, options => {
    options.LoginPath = "/Users/Login";
    options.AccessDeniedPath = "/Users/NotAuthorized";
});

builder.Services.AddScoped<IUserStore<UserDTO>, StoreUser>();
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SignInManager<UserDTO>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGroupsRepository, UserGroupsRepository>();
builder.Services.AddScoped<IGroupsRepository, GroupsRepository>();
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
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