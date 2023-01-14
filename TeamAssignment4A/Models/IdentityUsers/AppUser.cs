using Microsoft.AspNetCore.Identity;

namespace TeamAssignment4A.Models.IdentityUsers
{
    public class AppUser : IdentityUser
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        private bool _isAdmin;
        public bool IsAdmin { get; set; }

    }
}
