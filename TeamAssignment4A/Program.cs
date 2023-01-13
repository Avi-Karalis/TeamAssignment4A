using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TeamAssignment4A.Data;
using Microsoft.AspNetCore.Identity;


namespace TeamAssignment4A {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            var connectionString = builder.Configuration.GetConnectionString("OnLineServer") ?? throw new InvalidOperationException("Connection string 'OnLineServer' not found.");


            var optionsBuilder = new DbContextOptionsBuilder<WebAppDbContext>();

            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new WebAppDbContext(optionsBuilder.Options);
            builder.Services.AddDbContext<WebAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            //dbContext.Database.Migrate();
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();;

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}