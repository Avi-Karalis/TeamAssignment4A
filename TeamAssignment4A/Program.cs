using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using NuGet.Protocol.Core.Types;
using TeamAssignment4A.Controllers;
using TeamAssignment4A.Data;
using TeamAssignment4A.Data.Repositories;
using TeamAssignment4A.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TeamAssignment4A.Services;
using Duende.IdentityServer.Stores;
using TeamAssignment4A.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace TeamAssignment4A {
    public class Program {
        public void Configure(IApplicationBuilder app) {
            app.UseIdentityServer();
        }
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("OnLineServerAve") ?? throw new InvalidOperationException("Connection string 'OnLineServerAve' not found.");
            builder.Services.AddDbContext<WebAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WebAppDbContext>();
            builder.Services.AddAuthentication().AddIdentityServerJwt();

            X509Certificate2 certificate = new X509Certificate2("AverkiosCertificate.pfx", "adminadmin");

            builder.Services.AddIdentityServer().AddSigningCredential(certificate)
            .AddApiAuthorization<IdentityUser, WebAppDbContext>();



                builder.Services.AddControllersWithViews();

            //Authorization Handler and roles

            builder.Services.AddTransient<IAuthorizationHandler, AdminAuthorizationHandler>();
            builder.Services.AddTransient<IAuthorizationHandler, QAAuthorizationHandler>();
            builder.Services.AddTransient<IAuthorizationHandler, MarkerAuthorizationHandler>();
            builder.Services.AddTransient<IAuthorizationHandler, CandidateAuthorizationHandler>();
            

            builder.Services.AddTransient<IdentityDbContext<IdentityUser>, WebAppDbContext>();
            builder.Services.AddTransient<UnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<CandidateService, CandidateService>();
            builder.Services.AddTransient<CertificateService, CertificateService>();
            builder.Services.AddTransient<TopicService, TopicService>();
            builder.Services.AddTransient<StemService, StemService>();
            builder.Services.AddTransient<ExamService, ExamService>();
            builder.Services.AddTransient<ExamStemService, ExamStemService>();
            builder.Services.AddTransient<CandidateExamService, CandidateExamService>();
            builder.Services.AddAutoMapper(typeof(Program));
            
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy => {
                    policy.WithOrigins("https://localhost:44351/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });



            var app = builder.Build();

            using (var scope = app.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<WebAppDbContext>();
                context.Database.Migrate();  // Update-Database
                                             // requires using Microsoft.Extensions.Configuration;
                                             // Set password with the Secret Manager tool.
                                             // dotnet user-secrets set SeedUserPW <pw>
                var userPasswords = new UserPasswords() {
                    AdminPassword = builder.Configuration.GetValue<string>("AdminPW"),
                    QAPassword = builder.Configuration.GetValue<string>("QAPW"),
                    MarkerPassword = builder.Configuration.GetValue<string>("MarkerPW")
                };
               await SeedData.Initialize(services, userPasswords);
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/Images")
            });
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        
        }
    }
}