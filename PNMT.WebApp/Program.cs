using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using PNMT.ApiClient.Data;
using PNMT.WebApp.Authentification;
using PNMTD.Helper;
using PNMTD.Lib.Authentification;
using System.Runtime.InteropServices;

namespace PNMT.WebApp
{
    public class Program
    {
        private static readonly ILogger _logger = LogManager.CreateLogger<Program>();
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<PNMTDApi>();

            if (!Global.IsDevelopment && RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                builder.Configuration.AddJsonFile(Path.Combine(GlobalConfiguration.LinuxBasePath, "config.json"), true, true);
            }

            var requiredConfigurations = new string[] { "JwtToken", "apiurl" };

            foreach(var rc in requiredConfigurations)
            {
                if (builder.Configuration[rc] == null)
                {
                    _logger.LogError($"Missing {rc}");
                    if(!Global.IsDevelopment) return;
                }
            }

            if (Global.IsDevelopment)
            {
                builder.Services.AddSingleton<JwtTokenProvider>(new JwtTokenProvider(Global.IsDevelopment));
            }
            else
            {
                var tokenProvider = new JwtTokenProvider(Global.IsDevelopment);
                tokenProvider.JwtToken = builder.Configuration["JwtToken"];
                builder.Services.AddSingleton<JwtTokenProvider>(tokenProvider);
            }

            if(!Global.IsDevelopment)
            {
                PNMTDApi.BaseAddress = builder.Configuration["apiurl"];
            }
            

            builder.Services.AddControllers()
              .AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter() { }));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                });
            builder.Services.AddAuthorization();

            builder.Services.AddSingleton<WebAppUserManager>(new WebAppUserManager());

            builder.Services.AddMudServices();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var logFolder = "";
            if (!Global.IsDevelopment && RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                logFolder = Path.Combine(GlobalConfiguration.LinuxBasePath, "logs");
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }
            }

            builder.Logging.AddFile(Path.Combine(logFolder, "pnmt.log"), fileSizeLimitBytes: 52430000, retainedFileCountLimit: 10);

            var app = builder.Build();

            LogManager.App = app;

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.MapControllers();

            Global.WebApp = app;

            app.Run();
        }
    }
}