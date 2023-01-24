using TeamAssignment4A.Data;
using TeamAssignment4A.Data.Repositories;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class UnitOfWork : IDisposable
    {
        private readonly WebAppDbContext _db;
        public CandidateRepository Candidate { get; set; }
        public CertificateRepository Certificate { get; set; }
        public TopicRepository Topic { get; set; }
        public StemRepository Stem { get; set; }
        public UnitOfWork(WebAppDbContext db)
        {
            _db = db;
            _db.Database.EnsureCreated();
            Candidate = new CandidateRepository(db);
            Certificate = new CertificateRepository(db);
            Topic = new TopicRepository(db);
            Stem = new StemRepository(db);
        }

        public async Task<int> SaveAsync() 
        {
            return await _db.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
