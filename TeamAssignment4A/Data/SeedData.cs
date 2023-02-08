using TeamAssignment4A.Authorization;
using TeamAssignment4A.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Matching;


namespace TeamAssignment4A.Data {
    public class SeedData {

        public static async Task Initialize (IServiceProvider serviceProvider, UserPasswords userPasswords) {
            var context = serviceProvider.GetRequiredService<WebAppDbContext>();

            var adminID = await EnsureUser(serviceProvider, userPasswords.AdminPassword, "admin@admin.admin");
            await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

            var qaId = await EnsureUser(serviceProvider, userPasswords.QAPassword, "QA@QA.QA");
            await EnsureRole(serviceProvider, qaId, Constants.ContactQARole);

            var markerAnnaId = await EnsureUser(serviceProvider, userPasswords.MarkerAnnaPassword, "MarkerAnna@Marker.Marker");
            await EnsureRole(serviceProvider, markerAnnaId, Constants.ContactMarkerRole);
            
            var markerTomId = await EnsureUser(serviceProvider, userPasswords.MarkerTomPassword, "MarkerTom@Marker.Marker");
            await EnsureRole(serviceProvider, markerTomId, Constants.ContactMarkerRole);


        }



        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName) {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null) {
                user = new IdentityUser {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null) {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }


        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role) {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null) {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role)) {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null) {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

    }
}
