using TeamAssignment4A.Data;
using TeamAssignment4A.Data.Repositories;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class UnitOfWork : IDisposable
    {
        private WebAppDbContext _db;
        public IGenericRepository<Candidate> Candidate { get; set; }
        public IGenericRepository<Certificate> Certificate { get; set; }
        public IGenericRepository<Stem> Stem { get; set; }
        public UnitOfWork(WebAppDbContext db)
        {
            _db = db;
            Candidate = new CandidateRepository(db);
            Certificate = new CertificateRepository(db);
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
