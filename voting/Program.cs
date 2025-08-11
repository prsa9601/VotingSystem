using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VotingLibrary.Core.Services.Comands;
using VotingLibrary.Core.Services.Interfaces;
using VotingLibrary.Core.Services.Services;
using VotingLibrary.Data.Entities.Repository;
using VotingLibrary.Data.Persistent.Ef;
using VotingLibrary.Data.Persistent.Ef.Entities.Repository;
using VotingLibrary.Data.Persistent.Ef.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/Login";
    option.LogoutPath = "/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(1);
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/";
});

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       }));
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IElectionRepository, ElectionRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IElectionService, ElectionService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IVoteService, VoteService>();



// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
});
//builder.Services.AddRazorPages();
//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");
//});
builder.Services.AddRazorPages();
var app = builder.Build();

//using (var scope = new Context())
//{

//}


    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseHangfireDashboard("/dashboard");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
