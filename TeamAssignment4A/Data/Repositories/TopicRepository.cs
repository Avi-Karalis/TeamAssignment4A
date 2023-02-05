using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class TopicRepository : IGenericRepository<Topic>
    {
        private readonly WebAppDbContext _db;
        public TopicRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Topic?> GetAsync(int id)
        {
            return await _db.Topics.AsNoTracking().Include(topic => topic.Certificate)
                .Include(topic => topic.Stems).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Topic?> GetAsyncByDesc(string topicDescription)
        {
            return await _db.Topics.Include(topic => topic.Certificate).Include(topic => topic.Stems)
                .FirstOrDefaultAsync(x => x.Description == topicDescription);
        }

        public async Task<IEnumerable<Topic>?> GetAllAsync()
        {
            return await _db.Topics.Include(topic => topic.Certificate)
                .Include(topic => topic.Stems).ToListAsync<Topic>();
        }

        public EntityState AddOrUpdate(Topic topic)
        {
            _db.Topics.Update(topic);
            return _db.Entry(topic).State;
        }

        public void Delete(Topic topic)
        {
            _db.Topics.Remove(topic);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Topics.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> DescriptionExists(int id, string description)
        {
            return await _db.Topics.AnyAsync(e => e.Description == description && e.Id != id);
        }
    }
}
