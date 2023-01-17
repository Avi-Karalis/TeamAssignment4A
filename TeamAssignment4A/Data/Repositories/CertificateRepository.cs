using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Data.Repositories
{
    public class CertificateRepository : IGenericRepository<Certificate>
    {
        private readonly WebAppDbContext _db;
        public CertificateRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<Certificate?> GetAsync(int? id)
        {            
            return await _db.Certificates.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task<ICollection<Certificate?>> GetAllAsync()
        {
            return await _db.Certificates.ToListAsync<Certificate>();
        }        

        public async Task<int> AddOrUpdateAsync(Certificate? certificate)
        {            
            _db.Certificates.Update(certificate);
            try
            {                
                return certificate.Id;
            }
            catch (Exception)
            {
                return -1;
            }                        
        }

        public async Task<bool> DeleteAsync(int? id)
        {            
            try
            {
                _db.Certificates.Remove(await _db.Certificates.FirstOrDefaultAsync(x => x.Id == id));                
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
