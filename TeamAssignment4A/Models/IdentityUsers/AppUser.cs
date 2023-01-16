using Microsoft.AspNetCore.Identity;

namespace TeamAssignment4A.Models.IdentityUsers
{
    public class AppUser : IdentityUser
    {

        public Candidate Candidate { get; set; }
    }
}
