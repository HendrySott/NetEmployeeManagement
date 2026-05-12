using Microsoft.AspNetCore.Authentication.Cookies;
using NetEmployee.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<AccountController>();

var connectionString = builder.Configuration.GetConnectionString("DatabaceConectionString");
// builder.Services.Add(new ServiceDescriptor(typeof(UserAuthenticationServices), new UserAuthenticationServices(connectionString)));
builder.Services.Add(new ServiceDescriptor(typeof(DataServices), new DataServices(connectionString)));
builder.Services.Add(new ServiceDescriptor(typeof(DataServicesAuthentication), new DataServicesAuthentication(connectionString)));

// Add session services
    builder.Services.AddSession(options =>
    {
        // Set session timeout
        options.IdleTimeout = TimeSpan.FromHours(12); // Adjust timeout as needed
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true; // Make the session cookie essential
    });

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.Cookie.Name = "akhdflahs8d7a97gh4skd6a734khj3si43.Cookie";
//         options.Cookie.HttpOnly = true;
//         options.Cookie.SameSite = SameSiteMode.Strict;
//         options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Always in production
//         // Configure cookie encryption
//         options.Cookie.IsEssential = true;
//         // Configure encryption and decryption
//         options.CookieManager = new ChunkingCookieManager { ChunkSize = 3000 };

//     });

        builder.Services.AddAuthentication(options =>
        {
            // Configure default authentication scheme, e.g., cookies, JWT, etc.
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            // Configure settings for the authentication cookie
            options.Cookie.Name = "sjdoisajodo8s7ayf789a8sdajdlfjgsld";
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure cookies are sent over HTTPS
            options.LoginPath = "/Account/Login"; // URL where users are redirected when not authenticated
            options.LogoutPath = "/Account/Logout"; // URL where users are redirected after logout
            options.CookieManager = new ChunkingCookieManager { ChunkSize = 3000 };
        });




builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // app.UseCookiePolicy();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); // Add this line if you need authorization

app.MapRazorPages();

app.Run();
