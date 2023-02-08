using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Dtos;

namespace TeamAssignment4A.Data.Repositories
{
    public class UserRepository
    {
        private readonly WebAppDbContext _db;
        public UserRepository(WebAppDbContext context)
        {
            _db = context;
        }


        // Get User by Id
        public async Task<IdentityUser?> GetById(string UserId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == UserId);
        }


        // Create a new User with e-mail
        public async Task<IdentityUser> AddAsync(string email)
        {
            IdentityUser user = new IdentityUser();
            user.Email = email;
            await _db.Users.AddAsync(user);
            return user;
        }  
        
        public void Delete(IdentityUser user)
        {
            _db.Users.Remove(user);
        }
    }
}
