using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class CertificateRepository : IGenericRepository<Certificate>
    {
        private WebAppDbContext _db;
        public CertificateRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Certificate> Get(int? id)
        {
            return await _db.Certificates.FindAsync(id);
        }

        public async Task<ICollection<Certificate>> GetAll()
        {
            return await _db.Certificates.ToListAsync<Certificate>();
        }

        public async Task<Certificate> Add(Certificate? certificate)
        {
            await _db.Certificates.AddAsync(certificate);
            await _db.SaveChangesAsync();
            return certificate;
        }

        public async Task<Certificate> Update(Certificate? certificate)
        {
            if (await _db.Certificates.FindAsync(certificate.Id) != null)
            {
                _db.Certificates.Update(certificate);
                //_db.Entry(certificate).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return certificate;
            }
            return null;
        }

        public async Task<int> Delete(int? id)
        {
            int result = 0;
            if (await _db.Certificates.FindAsync(id) != null)
            {
                _db.Certificates.Remove(await _db.Certificates.FindAsync(id));
                await _db.SaveChangesAsync();
                result = 1;
            }
            return result;
        }
    }
}
