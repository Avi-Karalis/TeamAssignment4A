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

        // Get User by specific e-mail
        public async Task<IdentityUser?> GetAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        // Get those Users who have e-mail
        public async Task<IEnumerable<IdentityUser>?> GetAllAsync()
        {
            return await _db.Users.Where(user => user.Email != null).ToListAsync<IdentityUser>();
        }

        // Create a new User with e-mail
        public async Task<IdentityUser> AddAsync(string email)
        {
            IdentityUser user = new IdentityUser();
            user.Email = email;
            await _db.Users.AddAsync(user);
            return user;
        }     
    }
}
