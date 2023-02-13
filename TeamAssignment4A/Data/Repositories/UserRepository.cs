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
        public async Task<IdentityUser?> GetByEmail(string UserEmail)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == UserEmail);
        }

        // Get all Users who have e-mail
        public async Task<IEnumerable<IdentityUser>?> GetAllAsync()
        {
            return await _db.Users.Where(user => user.Email != null).ToListAsync<IdentityUser>();
        }

        public void Delete(IdentityUser user)
        {
            _db.Users.Remove(user);
        }
        public async Task<bool> EmailExists(string id, string userName)
        {
            var x = await _db.Users.AnyAsync(e => e.UserName == userName && e.Id != id);
            return x;
        }

        public async Task<bool> EmailExistsForCreate(string userName)
        {
            var x = await _db.Users.AnyAsync(e => e.UserName == userName);
            return x;
        }
    }
}
